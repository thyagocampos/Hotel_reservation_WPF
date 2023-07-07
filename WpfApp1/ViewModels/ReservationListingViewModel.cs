using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {        
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        
        private HotelStore _hotelStore;

        public ICommand LoadReservationsCommand { get; }
        
        public ICommand? MakeReservationCommmand { get; }

        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        private string _errorMessage;

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isLoading { get; set; }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }


        public ReservationListingViewModel(HotelStore hotelStore,  NavigationService makeReservationNavigationService)
        {
            _hotelStore = hotelStore;
         
            _reservations = new ObservableCollection<ReservationViewModel>();            

            LoadReservationsCommand = new LoadReservationCommand(hotelStore, this);
            MakeReservationCommmand = new NavigateCommand(makeReservationNavigationService);

            _hotelStore.ReservationMade += OnReservationMade;
        }

        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMade;
            base.Dispose();
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore,             
            NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);

            viewModel.LoadReservationsCommand.Execute(null);
             
            return viewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach(Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
