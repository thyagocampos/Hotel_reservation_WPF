using System.Collections.Generic;
using System.Linq;
using WpfApp1.Exceptions;

namespace WpfApp1.Models
{
    internal class ReservationBook
    {
        private readonly List<Reservation> _reservations;

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns>All reservations in reservation book.</returns>
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservations;
        }

        public void AddReservation(Reservation reservation)
        {
            foreach(Reservation existingReservation in _reservations)
            {
                if (existingReservation.Conflicts(reservation))
                {
                    throw new ReservationConflictException(existingReservation, reservation);
                }
            }

            _reservations.Add(reservation);
        }
    }
}
