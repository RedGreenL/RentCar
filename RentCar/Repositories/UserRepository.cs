using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        
         public UserRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }

    }
    
}