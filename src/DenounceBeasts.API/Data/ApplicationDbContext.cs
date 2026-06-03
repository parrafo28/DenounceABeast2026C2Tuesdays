using DenounceBeasts.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options ): base(options)
        { 
        }

        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
    }
}
