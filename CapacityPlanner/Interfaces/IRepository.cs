using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CapacityPlanner.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public bool Add(T entity);
        public List<T> GetAll();
        public T Get(int id);
        public bool Update(int id, T entity);
        public bool Delete(int id);
        public List<T> SearchAll(Expression<Func<T, bool>> searchMethod);
        public T Search(Expression<Func<T, bool>> searchMethod);
    }
}
