﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentCar.Models;
using RentCar.Repositories;

namespace RentCar.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Client GetByPhone(string searchPhone);
    }
}