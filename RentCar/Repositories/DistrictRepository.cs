using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class DistrictRepository: GenericRepository<District>, IDistrictRepository
    {
        
         public DistrictRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }

    }
    
}