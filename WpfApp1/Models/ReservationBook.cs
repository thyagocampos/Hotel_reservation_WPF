using System.Collections.Generic;
using System.Threading.Tasks;
using WpfApp1.Exceptions;
using WpfApp1.Services.ReservationConflictValidation;
using WpfApp1.Services.ReservationCreator;
using WpfApp1.Services.ReservationProvider;

namespace WpfApp1.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(
            IReservationProvider reservationProvider, 
            IReservationCreator reservationCreator, 
            IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns>All reservations in reservation book.</returns>        
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation =
                await _reservationConflictValidator.GetConflictingReservation(reservation);

            if (conflictingReservation != null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }           

            await _reservationCreator.CreateReservation(reservation);
        }
    }
}
