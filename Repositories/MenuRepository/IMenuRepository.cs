using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mYSelfERPWeb
{
    public interface IMenuRepository
    {
        List<Dictionary<string, object>> GetMenuList();
        List<Dictionary<string, object>> GetSubMenuList();
        List<Dictionary<string, object>> GetNestedMenuList();
        List<Dictionary<string, object>> GetModuleList();
    }
}
