using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class AddressRepository : GenericRepository<Addres>, IAddressRepository
    {
        
         public AddressRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }

    }
    
}