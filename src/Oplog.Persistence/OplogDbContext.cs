using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;

namespace Oplog.Persistence
{
    public class OplogDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ConfiguredType> ConfiguredTypes { get; set; }
        public DbSet<OperationArea> OperationAreas { get; set; }
        public DbSet<LogsView> LogsView { get; set; }
        public DbSet<CustomFilter> CustomFilters { get; set; }
        public DbSet<CustomFilterItem> CustomFilterItems { get; set; }
        public DbSet<LogTemplate> LogTemplates { get; set; }
        public DbSet<Unit> Units { get; set; }

        public OplogDbContext(DbContextOptions<OplogDbContext> context) : base(context)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .HasIndex(l => l.CreatedDate);

            modelBuilder.Entity<LogsView>()
                .ToView(nameof(LogsView))
                .HasKey(l => l.Id);
        }
    }
}
