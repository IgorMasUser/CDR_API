using CDR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CDR_API.Data
{
    public class CdrContext : DbContext
    {
        public DbSet<CallRecord> CallRecords { get; set; }

        public CdrContext(DbContextOptions<CdrContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CallRecord>(entity =>
            {
                entity.ToTable("CallRecords");

                entity.HasKey(e => e.CallerId);

                entity.Property(e => e.CallerId)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.Recipient)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.CallDate)
                    .IsRequired();

                entity.Property(e => e.EndTime)
                    .IsRequired();

                entity.Property(e => e.Duration)
                    .IsRequired();

                entity.Property(e => e.Cost)
                    .IsRequired()
                    .HasColumnType("decimal(18,3)");

                entity.Property(e => e.Reference)
                    .IsRequired();

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3);
            });
        }
    }
}
