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
    public interface ICarRepository : IGenericRepository<Car>
    {
        void UploadImageReturnID(int carId, HttpPostedFileBase file);
    }
}
