using InternetFanPage.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InternetFanPage.Services
{
    public class ConcertsService
    {
        public IList<Concert> searchConcert(string name, int? price, DateTime? searchTermDateStart, DateTime? searchTermDateEnd)
        {
            using (var context = new FanPageContext())
            {
                IQueryable<Concert> cons = context.Concerts.Where(p => p.City.ToLower().Contains(name.ToLower()));

                if (price != null)
                {
                    cons = cons.Where(p => p.Price <= price);
                }

                if (searchTermDateStart != null)
                {
                    cons = cons.Where(p => Convert.ToDateTime(p.Date).Date >= searchTermDateStart);
                }

                if (searchTermDateEnd != null)
                {
                    cons = cons.Where(p => Convert.ToDateTime(p.Date).Date <= searchTermDateEnd);
                }

                return cons.ToList();
            }
        }


    }
}