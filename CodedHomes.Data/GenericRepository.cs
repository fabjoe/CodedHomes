using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedHomes.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> _dbSet {get;set;}
        protected DbContext _context { get; set; }
        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DBContext is required to use this repository.", "context");
            }
            this._context = context;
            this._dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            DbEntityEntry entry = this._context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this._dbSet.Add(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = this.GetById(id);
            if(entity != null)
            {
                this.Delete(entity);
            }
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = this._context.Entry(entity);
            if(entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this._dbSet.Attach(entity);
                this._dbSet.Remove(entity);
            }
        }
        public void Detach(T entity)
        {
            DbEntityEntry entry = this._context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public IQueryable<T> GetAll()
        {
            return this._dbSet;
        }

        public T GetById(int id)
        {
            return this._dbSet.Find(id);
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this._context.Entry(entity);
            if(entry.State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }
            else
            {
                entry.State = EntityState.Modified;
            }
        }
    }
}
