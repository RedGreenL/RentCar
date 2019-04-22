using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        
         public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }

    }
    
}