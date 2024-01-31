using BankSystem.Core.Models;
using BankSystem.Core.Models.Domains;
using BankSystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
           return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetById(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Where(criteria).ToList();
        }

        public T GetByName(Expression<Func<T, bool>> criteria)
        {
           return _context.Set<T>().Where(criteria).FirstOrDefault();
        }

        public bool IsExist(Expression<Func<T, bool>>criteria)
        {
          var result= _context.Set<T>().Where(criteria).FirstOrDefault();
            if (result != null)
                return true;
            return false;
            
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }


    }
}
