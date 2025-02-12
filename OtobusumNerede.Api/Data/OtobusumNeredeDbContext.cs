using Microsoft.EntityFrameworkCore;
using OtobusumNerede.Api.Data.Entities;
using OtobusumNerede.Api.Data.Entities.GeoJson;

namespace OtobusumNerede.Api.Data
{
    public class OtobusumNeredeDbContext :DbContext
    {
        public OtobusumNeredeDbContext(DbContextOptions<OtobusumNeredeDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Durak> Duraklar { get; set; }
        public virtual DbSet<Hat> Hatlar { get; set; }
        public DbSet<OtobusRotasi> OtobusRotalari { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

    }
}
