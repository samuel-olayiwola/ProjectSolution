using System;
using System.Linq.Expressions;

namespace Project.Database.Repositories.Interfaces
{
    public interface IProjectRepository<T> where T : class
    {
        
            IEnumerable<T> GetAll();
            Task<T> GetById(int id);
            Task Add(T entity);
            Task Update(T entity);
            Task Delete(T entity);
            IEnumerable<T> GetPaged(int pageIndex, int pageSize, Expression<Func<T, bool>> filter, Expression<Func<T, object>> orderBy);
            



    }
}

