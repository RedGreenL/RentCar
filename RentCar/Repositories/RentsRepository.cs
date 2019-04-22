using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls.Expressions;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class RentsRepository : GenericRepository<Rents>, IRentsRepository
    {
        
         public RentsRepository(IDbFactory dbFactory) : base(dbFactory)
         {

         }
         public List<Rents> FindByPhone(string searchPhone)
         {
            Regex RemovalRegex = new Regex(@"\s|[().]");
            string fp = RemovalRegex.Replace(searchPhone, "");
            return  DbContext.Rents.Where(x => x.Client.Phone.Contains(fp)).ToList();
         }

        public double MoneyLastMonth()
        {
            try
            {
                return DbContext.Rents.Where(d => d.EndData.Month == DateTime.Now.Month
                                                  && d.StartData.Month == DateTime.Now.Month).Sum(m => m.Price);
            }
            catch (Exception)
            {
                return 0.0;
            }

        }

        public int RentsLastMonth()
        {
          return  DbContext.Rents.Where(d => d.EndData.Month == DateTime.Now.Month 
                                             && d.StartData.Month == DateTime.Now.Month).Count();
        }

        public Car Top1Car()
        {

            var carCount = from car in DbContext.Rents
                group car by car.Car_ID
                into red
                select new
                {
                    carID = red.Key,
                    count = red.Count()
                };
            
              try
              {
                  var topCarId = carCount.OrderByDescending(x => x.count).Select(x => x.carID).First();
                  return DbContext.Car.Where(x => x.Car_ID == topCarId).FirstOrDefault();
               }
              catch (Exception)
              {
                  return DbContext.Car.FirstOrDefault();
              }
              
        }

        public List<Rents> TopProductiveCar(int numberCars = 5)
        {
            var topCars = DbContext.Rents.Include(x=> x.Car).GroupBy(x => x.Car_ID).Select(g => new
             {
                 
                Car_ID = g.Key,
                Price = g.Sum(x => x.Price),
                Car = g.Select(x=>x.Car).FirstOrDefault(),
               }
            ).OrderByDescending(x => x.Price).Take(numberCars)
                .AsEnumerable().Select(x => new Rents
            {
                Car_ID = x.Car_ID,
                Price = x.Price,
                Car = x.Car
            }).ToList();

            return topCars;
        }
    }
    
}