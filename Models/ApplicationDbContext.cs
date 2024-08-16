using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace SignalRCRUDPractice.Models
{
    public class ApplicationDbContext:DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public virtual DbSet<Products> Products { get; set; }
    }
}
