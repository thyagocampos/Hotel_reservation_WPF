using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Services;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private string _userName = "";
        private string _roomNumber = "";
        private string _floorNUmber = "" ;
        private DateTime _startDate =  DateTime.Today;
        private DateTime _endDate = DateTime.Today;

        public string UserName
        {
            get { return _userName; }

            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string RoomNumber
        {
            get { return _roomNumber; }

            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        public string FloorNumber
        {
            get { return _floorNUmber; }

            set
            {
                _floorNUmber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }

            set
            {
                _startDate = value;

                OnPropertyChanged(nameof(StartDate));
                
                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("The start date cannot be after the end date.", nameof(StartDate));
                }

            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }

            set
            {
                _endDate = value;

                OnPropertyChanged(nameof(EndDate));
                
                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("The end date cannot be before the start date.", nameof(EndDate));                    
                }
            }
        }
       
        public ICommand? SubmitCommand { get; } 

        public ICommand? CancelCommand { get; }

        public  MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
            CancelCommand = new NavigateCommand(reservationViewNavigationService);

            _propertyNameToErrorDictionary = new Dictionary<string, List<string>>();
        }

        //INotifyDataErrorInfo errors memebers

        private readonly Dictionary<string, List<string>> _propertyNameToErrorDictionary;

        public bool HasErrors
        {
            get
            {
                return _propertyNameToErrorDictionary.Any();

            }
        } 

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void ClearErrors(string propertyName)
        {         
            _propertyNameToErrorDictionary.Remove(propertyName);
            
            OnErrorsChange(propertyName);
        }

        private void OnErrorsChange(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if(!_propertyNameToErrorDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorDictionary[propertyName].Add(errorMessage);

            OnErrorsChange(propertyName);
        }
    }
}
