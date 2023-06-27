using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Exceptions;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Stores;
using WpfApp1.ViewModels;

namespace WpfApp1.Commands
{
    internal class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationService _reservationViewNavigationService;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, 
            HotelStore hotelStore, NavigationService reservationViewNavigationService)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotelStore = hotelStore;
            _reservationViewNavigationService = reservationViewNavigationService;
            
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_makeReservationViewModel.UserName)||
                e.PropertyName == nameof(_makeReservationViewModel.FloorNumber))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.UserName) && 
                _makeReservationViewModel.FloorNumber != "0" &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(int.Parse(_makeReservationViewModel.FloorNumber), int.Parse(_makeReservationViewModel.RoomNumber.ToString())),
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate, 
                _makeReservationViewModel.UserName);

            try
            {

                await _hotelStore.MakeReservation(reservation);

                MessageBox.Show("Successfully reserved room", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _reservationViewNavigationService.Navigate();

            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to make reservation", "Error",
                 MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
