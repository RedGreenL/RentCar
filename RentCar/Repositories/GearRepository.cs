using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class GearRepository : GenericRepository<GearType>, IGearRepository
    {
        
         public GearRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }

    }
    
}