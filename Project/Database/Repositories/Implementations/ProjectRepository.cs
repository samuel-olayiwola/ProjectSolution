using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project.Database.DbContexts;
using Project.Database.Repositories.Interfaces;

namespace Project.Database.Repositories.Implementations
{
    public class ProjectRepository<T> : IProjectRepository<T> where T : class
    {

        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<ProjectRepository<T>> _logger;

        public ProjectRepository(AppDbcontext context, ILogger<ProjectRepository<T>> logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        //default get all entity
        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        //implementation of paging, sorting and filtering with page size defaulted to 10
        public IEnumerable<T> GetPaged(int pageIndex =1 , int pageSize = 10, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        
        //get entity by ID
        public async Task<T> GetById(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync();
        }

        //Create new entity
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            LogActivity("Insert");
            
        }

        //update entity
        public async Task Update(T entity)
        {
            
            _context.Entry(entity).CurrentValues.SetValues(entity);
            
            _context.Update(entity);
            await _context.SaveChangesAsync();
            LogActivity("Update");
        }

        //delete entity
        public async Task Delete(T entity)
        {
           
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        //log operations
         private void LogActivity(string activity)
    {
        _logger.LogInformation("{OperationType} operation performed at {DateTime}", activity, DateTime.UtcNow);
    }

    }
}

