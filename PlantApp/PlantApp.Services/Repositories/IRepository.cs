using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Services.Repositories
{
    interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int id);
        List<T> GetAll();
    }
}
