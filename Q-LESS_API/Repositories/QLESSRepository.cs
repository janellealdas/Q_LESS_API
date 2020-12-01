using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QLESS.DBContext;
using QLESS.Entities;
using QLESS.Models;

namespace QLESS.Repository
{
    public class QLESSRepository : IQLESSRepository
    {
        private readonly QLESSDBContext DbContext;
        private Boolean Disposed;
        public String Connectionstring { get; }
        
        public QLESSRepository(QLESSDBContext dbContext, IOptions<AppSettings> appSettings)
        {
            DbContext = dbContext;
            Connectionstring = appSettings.Value.ConnectionString;
        }
        
        public void Dispose()
        {
            if (!Disposed)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();

                    Disposed = true;
                }
            }
        }

        public IQueryable<TransportCard> GetTransportCardDetails(int transportCardNumber)
        {
            var query = DbContext.Set<TransportCard>()
                        .AsQueryable()
                        .Where(item => item.TransportCardNumber == transportCardNumber)
                        .Select(item => item);

            return query;
        }

        public IQueryable<MRTLine> GetMRTLineStations()
        {
            var query = DbContext.Set<MRTLine>()
                        .AsQueryable()
                        .Select(item => item);

            return query;
        }

        public IQueryable<Stations> GetStationsFromTo(int mrtLineNumber)
        {
            var query = DbContext.Set<Stations>()
                        .AsQueryable()
                        .Where(item => item.MRTLineNumber == mrtLineNumber)
                        .Select(item => item);

            return query;
        }

        public IQueryable<ComputedFare> GetComputedFare(string stationFrom, string stationTo)
        {
            List<ComputedFare> response = new List<ComputedFare>();

            using (var conn = new System.Data.SqlClient.SqlConnection(Connectionstring))
            {
                conn.Open();

                using (var command = new System.Data.SqlClient.SqlCommand("dbo.GetComputedFare_sp", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("StationFrom", stationFrom);
                    command.Parameters.AddWithValue("StationTo", stationTo);
                    var rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        response.Add(new ComputedFare()
                        {
                            Amount = Convert.ToInt32(rdr["Amount"])
                           
                        });
                    }
                }

                conn.Close();
            }
            return response.AsQueryable();
        }

        public async Task<Responses> RequestDiscount(int transportCardNumber, string idNumber)
        {
            Responses resp = new Responses();

            var transportCard = DbContext.Set<TransportCard>()
                         .AsQueryable()
                         .Where(item => item.TransportCardNumber == transportCardNumber)
                         .FirstOrDefault();

            transportCard.Discount = 20;
            transportCard.IDNumber = idNumber;
            transportCard.IsRegistered = true;


            await DbContext.SaveChangesAsync();

            resp.Response = "Success! You may now enjoy your discount.";

            return resp;
        }

        public async Task<Responses> SwipeCard(int transportCardNumber, int fare)
        {
            Responses resp = new Responses();

            decimal discounts = 0;
            int dataCount = 0;
            DateTime dateToday = DateTime.Today;

            var transportCard = DbContext.Set<TransportCard>()
                         .AsQueryable()
                         .Where(item => item.TransportCardNumber == transportCardNumber)
                         .FirstOrDefault();

            var transportCardTransactions = DbContext.Set<TransportCardTransactions>()
                         .AsQueryable()
                         .Where(item => item.TransportCardNumber == transportCardNumber && item.Date == dateToday);

            var transactionsLast = transportCardTransactions.LastOrDefault();

            dataCount = transportCardTransactions.Count();

            if (dataCount <= 4)
            {
                discounts = (dataCount==0) ? transportCard.Discount : transactionsLast.Discount;
            }
            
            transportCard.Balance = transportCard.Balance - (fare - (fare * (discounts/100)));
            transportCard.NumberofUse ++;
            transportCard.LastUsed = dateToday;

            var newTransportCardTransactions = new TransportCardTransactions {

                TransportCardNumber = transportCardNumber,
                Balance = transportCard.Balance,
                Discount = ((dataCount == 0) ? transportCard.Discount : discounts) + ((dataCount <= 4) ? 3 : 0),
                Date = dateToday

            };

            DbContext.Add<TransportCardTransactions>(newTransportCardTransactions);

            await DbContext.SaveChangesAsync();

            resp.Response = "Success! Your new balance as of  "+ dateToday + " is PHP" +transportCard.Balance;

            return resp;
        }
    }
}
