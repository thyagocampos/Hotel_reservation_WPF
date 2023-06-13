using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public ICommand? MakeReservationCommmand { get; }

        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ReservationListingViewModel()
        {
            _reservations = new ObservableCollection<ReservationViewModel>();

            MakeReservationCommmand = new NavigateCommand();

            //_reservations.Add(new ReservationViewModel(new Reservation(new RoomID(1, 2), DateTime.Now, DateTime.Now.AddDays(5), "Thyago")));
            //_reservations.Add(new ReservationViewModel(new Reservation(new RoomID(2, 1), DateTime.Now, DateTime.Now.AddDays(6), "Thyago")));
            //_reservations.Add(new ReservationViewModel(new Reservation(new RoomID(3, 4), DateTime.Now, DateTime.Now.AddDays(7), "Thyago")));

        }
    }
}
