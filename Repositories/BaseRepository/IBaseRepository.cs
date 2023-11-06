using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mYSelfERPWeb
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        void DeleteRange(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IEnumerable<T> GetAllAsEnumerable();
        IQueryable<T> GetAllData();
        IQueryable<T> All { get; }
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        List<T> GetList(Expression<Func<T, bool>> predicate);
    }
}
