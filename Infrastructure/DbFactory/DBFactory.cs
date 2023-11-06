using Autofac.Util;

namespace mYSelfERPWeb
{
    public class DbFactory : Disposable, IDbFactory
    {
        DatabaseContext.DatabaseContext _dbContext;

        public DatabaseContext.DatabaseContext Init()
        {
            return _dbContext ?? (_dbContext = new DatabaseContext.DatabaseContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}