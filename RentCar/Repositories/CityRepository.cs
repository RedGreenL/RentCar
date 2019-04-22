using RentCar.Interfaces;
using RentCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCar.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public IEnumerable<District> GetAllDistricts()
        {
            return this.DbContext.District.ToList();
        }
    }

}