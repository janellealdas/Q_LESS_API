using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLESS.Models;
using QLESS.Repository;
using QLESS.Entities;


namespace QLESS.Controllers
{
    [Route("api/[controller]/")]

    public class QLESSController : Controller
    {
        private IQLESSRepository QLESSRepository;

        public QLESSController(IQLESSRepository repository)
        {
            QLESSRepository = repository;
        }

        protected override void Dispose(Boolean disposing)
        {
            QLESSRepository?.Dispose();

            base.Dispose(disposing);
        }

        [HttpGet]
        public string GetValue()
        {
            string value = "Test";
            return value;
        }

        [HttpGet]
        [Route("GetTransportCardDetails")]
        public async Task<IActionResult> GetTransportCardDetails(int transportCardNumber)
        {
            try
            {
                IQueryable<TransportCard> response = QLESSRepository.GetTransportCardDetails(transportCardNumber);
                return Ok(response);
                
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetMRTLineStations")]
        public async Task<IActionResult> GetMRTLineStations()
        {
            try
            {
                IQueryable<MRTLine> response = QLESSRepository.GetMRTLineStations();
                return Ok(response);
               
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("GetStationsFromTo")]
        public async Task<IActionResult> GetStationsFromTo(int mrtLineNumber)
        {
            try
            {
                IQueryable<Stations> response = QLESSRepository.GetStationsFromTo(mrtLineNumber);
                return Ok(response);

            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("GetComputedFare")]
        public async Task<IActionResult> GetComputedFare(string stationFrom, string stationTo)
        {
            try
            {
                IQueryable<ComputedFare> response = QLESSRepository.GetComputedFare(stationFrom, stationTo);
                return Ok(response);

            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPut]
        [Route("RequestDiscount")]
        public async Task<IActionResult> RequestDiscount(int transportCardNumber, string idNumber)
        {
            try
            {
                var response = await QLESSRepository.RequestDiscount(transportCardNumber, idNumber);
                return Ok(response);

            }
            catch (Exception e)
            {   
                throw;
            }

        }

        [HttpPut]
        [Route("SwipeCard")]
        public async Task<IActionResult> SwipeCard(int transportCardNumber, int fare)
        {
            try
            {
                var response = await QLESSRepository.SwipeCard(transportCardNumber, fare);
                return Ok(response);

            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
