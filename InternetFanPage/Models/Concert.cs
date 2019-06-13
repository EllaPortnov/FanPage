using System;

namespace InternetFanPage.Models
{
    public class Concert
    {
        public int ConcertID { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}