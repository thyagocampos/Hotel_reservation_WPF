using System.ComponentModel;
using System.Windows;
using WpfApp1.Exceptions;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Commands
{
    internal class MakeReservationCommand : CommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly Hotel _hotel;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, Hotel hotel)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotel = hotel;

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

        public override void Execute(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(int.Parse(_makeReservationViewModel.FloorNumber), int.Parse(_makeReservationViewModel.RoomNumber.ToString())),
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate, 
                _makeReservationViewModel.UserName);

            try
            {
                _hotel.MakeReservation(reservation);

                MessageBox.Show("Successfully reserved room", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }                        
        }
    }
}
