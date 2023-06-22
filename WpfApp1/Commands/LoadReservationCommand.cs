using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Commands
{
    public class LoadReservationCommand : AsyncCommandBase
    {
        private readonly Hotel _hotel;
        private readonly ReservationListingViewModel _viewModel;

        public LoadReservationCommand(Hotel hotel, ReservationListingViewModel viewModel)
        {
            _hotel = hotel;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

                _viewModel.UpdateReservations(reservations);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load reservation", "Error",
                 MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }
    }
}
