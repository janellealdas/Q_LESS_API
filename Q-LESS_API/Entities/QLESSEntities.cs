using System;

namespace QLESS.Entities
{

    public class TransportCard
    {
        public int Id { get; set; }

        public int TransportCardNumber { get; set; }

        public decimal Balance { get; set; }

        public decimal Discount { get; set; }

        public int NumberofUse { get; set; }

        public string IDNumber { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime DateofPurchase { get; set; }

        public DateTime? LastUsed { get; set; }
    }

    public class TransportCardTransactions
    {
        public int Id { get; set; }

        public int TransportCardNumber { get; set; }

        public decimal Balance { get; set; }

        public decimal Discount { get; set; }

        public DateTime Date { get; set; }
        
    }

    public class MRTLine
    {
        public int Id { get; set; }

        public string MRTLines { get; set; }
    }

    public class Stations
    {
        public int Id { get; set; }

        public int MRTLineNumber { get; set; }

        public string StationName { get; set; }
       
    }
}
