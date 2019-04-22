using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class FuelRepository: GenericRepository<FuelType>, IFuelRepository
    {
        
         public FuelRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }
    }
    
}