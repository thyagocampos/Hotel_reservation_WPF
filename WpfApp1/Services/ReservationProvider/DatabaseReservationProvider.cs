using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfApp1.DbContexts;
using WpfApp1.DTO;
using WpfApp1.Models;

namespace WpfApp1.Services.ReservationProvider
{
    internal class DatabaseReservationProvider : IReservationProvider
    {

        private readonly ReservroomDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(ReservroomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using(ReservRoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();

                return reservationDTOs.Select(r => ToReservation(r));
            }
        }

        private static Reservation ToReservation(ReservationDTO r)
        {
            return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber)
                ,r.StartTime,r.EndTime,r.UserName);
        }
    }
}
