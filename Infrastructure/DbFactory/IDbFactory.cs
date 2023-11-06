using System;

namespace mYSelfERPWeb
{
    public interface IDbFactory : IDisposable
    {
        DatabaseContext.DatabaseContext Init();
    }
}
