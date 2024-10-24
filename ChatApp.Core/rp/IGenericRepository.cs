﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ChatApp.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
