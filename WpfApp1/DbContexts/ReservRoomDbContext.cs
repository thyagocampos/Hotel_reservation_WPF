using Microsoft.EntityFrameworkCore;
using WpfApp1.DTO;

namespace WpfApp1.DbContexts
{
    public class ReservRoomDbContext : DbContext
    {
        public DbSet<ReservationDTO> Reservations { get; set; }


        public ReservRoomDbContext(DbContextOptions options) : base(options)
        {
            
        }

    }
}
