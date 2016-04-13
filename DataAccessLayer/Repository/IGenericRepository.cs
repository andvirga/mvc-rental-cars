using Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface IGenericRepository<T>
    {

        IEnumerable<T> GetAll();

        T GetByID(long pID);

        T Create(T pEntity);

        T Update(T pEntity);

        void Delete(T pEntity);
    }
}