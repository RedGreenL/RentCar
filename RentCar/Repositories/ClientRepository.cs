using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        
         public ClientRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }

         public Client GetByPhone(string searchPhone)
         {
             var getbyPhone = DbContext.Client.Where(x => x.Phone.Contains(searchPhone)).FirstOrDefault();
             if (getbyPhone == null)
             {
                 return null;
             }
             return getbyPhone;
         }

    }
    
}