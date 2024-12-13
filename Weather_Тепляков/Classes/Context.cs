using Microsoft.EntityFrameworkCore;

namespace Weather_Тепляков.Classes
{
    public class Context : DbContext
    {
        public DbSet<DataWeather> DataWeather { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql("server=localhost;database=pr8;user=root;", new MySqlServerVersion(new System.Version(8,0,11)));
    }
}
