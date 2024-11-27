using Microsoft.EntityFrameworkCore;
using TestDemo1.Model;

namespace TestDemo1.Data
{
    public class CarDbContext:DbContext
    {
        public CarDbContext(DbContextOptions options):base (options) 
        { 
        }
        public DbSet<Car> Car { get; set; } 
    }
}
