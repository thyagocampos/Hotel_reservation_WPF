using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name { get; set; }

        public Hotel(string name)
        {
            Name = name;

            _reservationBook = new ReservationBook();
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns>All reservations in reservation book.</returns>
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationBook.GetAllReservations();
        }

        /// <summary>
        /// Make a reservation
        /// </summary>
        /// <param name="reservation">Receives a reservation and add it to reservation book.</param>

        public void MakeReservation(Reservation reservation)
        {
            _reservationBook.AddReservation(reservation);
        }
    }
}
