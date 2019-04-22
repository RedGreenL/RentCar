using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using RentCar.Interfaces;
using RentCar.Models;

namespace RentCar.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {

        public CarRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void Create(Car car)
        {
            Car carr = new Car()
            {
                Category_ID = car.Category_ID,
                Brand = car.Brand,
                Color = "Red",
                Engine = car.Engine,
                FuelType_ID = car.FuelType_ID,
                GearType_ID = car.GearType_ID,
                Model = car.Model,
                Price = car.Price,
                Year = car.Year
            };                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            this.DbContext.Car.Add(carr);
            this.DbContext.SaveChanges();
        }

        public void UploadImageReturnID(int carId, HttpPostedFileBase file)
        {
            Images img = new Images();
            var allowedExtensions = new[]
            {
                ".Jpg", ".png", ".jpg", "jpeg"
            };
            img.Car_ID = carId;
            img.Path = file.ToString(); //getting complete url  
            var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
            var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
            if (allowedExtensions.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                string myfile = name + "_" + img.Images_ID + ext; //appending the name with id  
                // store the file inside ~/project folder(Img)  
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Img"), myfile);
                img.Path = Path.GetFileName(file.FileName);
                this.DbContext.Image.Add(img);
                this.DbContext.SaveChanges();
                file.SaveAs(path);
                
            }
        }

    }
}