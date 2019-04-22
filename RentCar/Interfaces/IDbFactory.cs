using RentCar.Models;
using System;

namespace RentCar.Interfaces
{
    public interface IDbFactory : IDisposable
    {

        ApplicationDbContext Init();
    }
}