using ChatApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ChatServer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _context;
        protected DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T GetById(object id) => _dbSet.Find(id);

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).ToList();

        public void Add(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;

        public void Remove(T entity) => _dbSet.Remove(entity);
    }
}
