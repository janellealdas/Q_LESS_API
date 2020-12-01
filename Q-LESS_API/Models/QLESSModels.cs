using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLESS.Models
{
    public class TransportCardDetails
    {
        public int TransportCardNumber { get; set; }

        public int Discount { get; set; }

        public int NumberofUse { get; set; }

        public string IDNumber { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime DateofPurchase { get; set; }

        public DateTime LastUsed { get; set; }

    }

    public class ComputedFare
    {
        public int Amount { get; set; }

    }

    public class Responses
    {
        public string Response { get; set; }
    }
}
