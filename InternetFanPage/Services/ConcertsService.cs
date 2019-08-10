using InternetFanPage.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InternetFanPage.Services
{
    public class ConcertsService
    {
        public IList<Concert> searchConcert(string name, int? price)
        {
            using (var context = new FanPageContext())
            {
                if (price != null)
                {
                    return context.Concerts.Where(p => (p.City.Contains(name)) && p.Price <= price).ToList();
                }
                else
                    return context.Concerts.Where(p => p.City.Contains(name)).ToList();
            }
        }


    }
}