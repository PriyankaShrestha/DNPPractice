using Microsoft.EntityFrameworkCore;
using WebApi.DataContainer;

namespace WebApi.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<VolumeResult> Volume { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = VolumeDB.db");
        }
    }
}