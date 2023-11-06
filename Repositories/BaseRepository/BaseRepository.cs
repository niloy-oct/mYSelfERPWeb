using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using mYSelfERPWeb.DatabaseContext;

namespace mYSelfERPWeb
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private DatabaseContext.DatabaseContext _dbContext;

        #region Properties

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected DatabaseContext.DatabaseContext DbContext
        {
            get { return _dbContext ?? (_dbContext = DbFactory.Init()); }
        }

        public BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        #endregion

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            DbContext.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = DbContext.Set<T>().Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                DbContext.Set<T>().Remove(obj);
        }

        public virtual void DeleteRange(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objectsToDelete = DbContext.Set<T>().Where(where).AsEnumerable();
            DbContext.Set<T>().RemoveRange(objectsToDelete);
        }


        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public virtual IEnumerable<T> GetAllAsEnumerable()
        {
            return DbContext.Set<T>().AsEnumerable();
        }

        public virtual IQueryable<T> GetAllData()
        {
            return DbContext.Set<T>();
        }

        public virtual IQueryable<T> All
        {
            get
            {
                PropertyInfo propInfo = typeof(T).GetProperty("ConcernID");
                if (propInfo != null)
                {
                    return null;
                }
                else
                {
                    return DbContext.Set<T>();
                }
            }
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate);
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate).ToList();
        }
    }
}