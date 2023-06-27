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
        
        public ICommand LoadReservationsCommand { get; }
        
        public ICommand? MakeReservationCommmand { get; }

        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ReservationListingViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {                       
            _reservations = new ObservableCollection<ReservationViewModel>();

            LoadReservationsCommand = new LoadReservationCommand(hotelStore, this);
            MakeReservationCommmand = new NavigateCommand(makeReservationNavigationService);            
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
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
