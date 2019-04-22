using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RentCar.Models;
using RentCar.Repositories;

namespace RentCar.Interfaces
{
    public interface IRentsRepository : IGenericRepository<Rents>
    {
        Car Top1Car();
        int RentsLastMonth();
        double MoneyLastMonth();
        List<Rents> TopProductiveCar(int numberCars = 5);
        List<Rents> FindByPhone(string searchPhone);

    }
}
