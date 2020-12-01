using Microsoft.EntityFrameworkCore;
using QLESS.Entities;

namespace QLESS.DataMappers
{

    public static class QLESSMapper
    {

        public static ModelBuilder MapTransportCard(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TransportCard>();

            entity.ToTable("TransportCard", "dbo");

            entity.HasKey(p => new { p.Id });

            entity.Property(p => p.Id).UseSqlServerIdentityColumn();

            return modelBuilder;
        }

        public static ModelBuilder MapTransportCardTransactions(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TransportCardTransactions>();

            entity.ToTable("TransportCardTransactions", "dbo");

            entity.HasKey(p => new { p.Id });

            entity.Property(p => p.Id).UseSqlServerIdentityColumn();

            return modelBuilder;
        }

        public static ModelBuilder MapMRTLineNumber(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<MRTLine>();

            entity.ToTable("MRTLine", "dbo");

            entity.HasKey(p => new { p.Id });

            entity.Property(p => p.Id).UseSqlServerIdentityColumn();

            return modelBuilder;
        }

        public static ModelBuilder MapStations(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Stations>();

            entity.ToTable("Stations", "dbo");

            entity.HasKey(p => new { p.Id });

            entity.Property(p => p.Id).UseSqlServerIdentityColumn();

            return modelBuilder;
        }
    }
}
