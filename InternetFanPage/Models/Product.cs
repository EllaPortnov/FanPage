﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetFanPage.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public int Inventory { get; set; }

        public string Name { get; set; }

        public string Image
        {
            get;
            set;
        }
    }
}