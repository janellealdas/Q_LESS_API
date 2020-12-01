using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLESS.Entities;
using QLESS.Models;

namespace QLESS.Repository

{
    public interface IQLESSRepository : IDisposable
    {
        IQueryable<TransportCard> GetTransportCardDetails(int transportCardNumber);
        IQueryable<MRTLine> GetMRTLineStations();
        IQueryable<Stations> GetStationsFromTo(int mrtLineNumber);
        IQueryable<ComputedFare> GetComputedFare(string stationFrom, string stationTo);
        Task<Responses> RequestDiscount(int transportCardNumber, string idNumber);
        Task<Responses> SwipeCard(int transportCardNumber, int fare);
    }
}
