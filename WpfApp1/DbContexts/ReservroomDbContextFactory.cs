using Microsoft.EntityFrameworkCore;

namespace WpfApp1.DbContexts
{
    public class ReservroomDbContextFactory
    {
        private readonly string _connectionString;

        public ReservroomDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReservRoomDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(_connectionString).Options;

            return new ReservRoomDbContext(options);

        }
    }
}
