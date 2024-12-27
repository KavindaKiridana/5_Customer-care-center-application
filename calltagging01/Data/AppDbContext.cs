using Microsoft.EntityFrameworkCore;
using calltagging01.Models;

namespace calltagging01.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Call> Calls { get; set; }
    }
}
