using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public ICommand? MakeReservationCommmand { get; }

        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ReservationListingViewModel(NavigationStore navigationStore, Func<MakeReservationViewModel> createMakeReservationViewModel)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();

            MakeReservationCommmand = new NavigateCommand(navigationStore, createMakeReservationViewModel);
        }
    }
}
