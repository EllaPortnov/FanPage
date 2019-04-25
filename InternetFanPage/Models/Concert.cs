using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetFanPage.Models
{
    public class Concert
    {
        public int ConcertID { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }
    }
}