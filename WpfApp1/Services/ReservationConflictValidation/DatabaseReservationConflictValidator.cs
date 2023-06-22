using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WpfApp1.DbContexts;
using WpfApp1.DTO;
using WpfApp1.Models;

namespace WpfApp1.Services.ReservationConflictValidation
{
    internal class DatabaseReservationConflictValidator : IReservationConflictValidator
    {

        private readonly ReservroomDbContextFactory _dbContextFactory;
        public DatabaseReservationConflictValidator(ReservroomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Reservation?> GetConflictingReservation(Reservation reservation)
        {
            using (ReservRoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO? reservationDTO = await context
                    .Reservations
                    .Where(r=>r.FloorNumber == reservation.RoomID.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
                    .Where(r => r.EndTime > reservation.StartTime)
                    .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefaultAsync();

                if(reservationDTO == null)
                {
                    return null;
                }

                return ToReservation(reservationDTO);
            }
        }
        private static Reservation ToReservation(ReservationDTO r)
        {
            return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber)
                , r.StartTime, r.EndTime, r.UserName);
        }
    }
}
