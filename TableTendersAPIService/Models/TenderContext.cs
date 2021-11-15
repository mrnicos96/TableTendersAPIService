using Microsoft.EntityFrameworkCore;

namespace TableTendersAPIService.Models
{
    public class TenderContext : DbContext
    {
        public DbSet<Tender> Tenders { get; set; }
        public TenderContext(DbContextOptions<TenderContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
