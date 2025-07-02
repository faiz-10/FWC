using FWC.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FWC.API.Data
{
    public class FWCDbContext : DbContext
    {
        public FWCDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
