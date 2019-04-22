using System;
using System.Collections.Generic;
using System.Linq;
using RentCar.Models;

namespace RentCar.Repositories
{
    public interface IGenericRepository<TEntity> 
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(int id);



        void Create(TEntity entity);

        void Update(int id, TEntity entity);

        void Delete(int id);
        void Save();
    }
}