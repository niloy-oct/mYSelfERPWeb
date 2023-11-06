using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mYSelfERPWeb.Services
{
    public interface IMiscellaneousService<T> where T : class
    {
        string GetUniqueKey(Func<T, int> codeSelector);
        string GetConcernUniqueKey(Func<T, int> codeSelector);
        T GetDuplicateEntry(Expression<Func<T, bool>> duplicateSelector);

    }
}
