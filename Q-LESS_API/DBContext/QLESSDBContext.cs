using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QLESS.DataMappers;

namespace QLESS.DBContext
{
    public class QLESSDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public QLESSDBContext(IOptions<AppSettings> appSettings)
        {
            ConnectionString = appSettings.Value.ConnectionString;
        }


        public String ConnectionString { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapTransportCard();
            modelBuilder.MapTransportCardTransactions();
            modelBuilder.MapMRTLineNumber();
            modelBuilder.MapStations();

            base.OnModelCreating(modelBuilder);
        }
    }
}
