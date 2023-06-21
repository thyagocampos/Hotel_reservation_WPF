using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DbContexts
{
    internal class ReservoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservRoomDbContext>
    {
        public ReservRoomDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=reservoom.db").Options;

            return new ReservRoomDbContext(options);

        }
    }
}
