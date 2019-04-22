using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        
         public DiscountRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }

        public float PercentForDays(int days)
        {
            float c = 1;
            var a = DbContext.Discounts.Where(x => x.Days <= days);
            if (a != null)
            {c =  a.OrderByDescending(x => x.Days).Take(1)
                    .Select(x => x.Percent).FirstOrDefault();
            }

            return c;
        }
    }
    
}