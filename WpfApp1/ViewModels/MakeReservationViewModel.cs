using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
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
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }

            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public ICommand? SubmitCommand { get; } 

        public ICommand? CancelCommand { get; }

        public  MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
            CancelCommand = new NavigateCommand(reservationViewNavigationService);
        }
    }
}
