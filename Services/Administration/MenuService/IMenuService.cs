using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public interface IMenuService
    {
        List<Dictionary<string, object>> GetMenuList();
        List<Dictionary<string, object>> GetSubMenuList();

        List<Dictionary<string, object>> GetNestedMenuList();
        List<Dictionary<string, object>> GetModuleList();
        void AddUserMenu(UserMenu userMenus);
        void SaveUserMenu();
        void DeleteUserMenuByUserTypeId(List<int> ids);
        List<UserMenu> GetUserMenuByUserTypeId(int id);
        void AddMenu(Menu menu);
        void UpdateMenu(Menu menu);
        void SaveMenu();
        IEnumerable<Menu> GetAllMenu();
        void AddSubMenu(SubMenu subMenu);
        void UpdateSubMenu(SubMenu subMenu);
        void SaveSubMenu();
        void AddNestedMenu(NestedMenu nestedMenu);
        void UpdateNestedMenu(NestedMenu nestedMenu);
        void SaveNestedMenu();
        IEnumerable<SubMenu> GetAllSubMenu();
        IEnumerable<NestedMenu> GetAllNestedMenu();
        Menu GetMenuById(int id);
        SubMenu GetSubMenuById(int id);
        IEnumerable<UserMenuViewModel> GetAllMenuByUserRole(int roleId);
        NestedMenu GetNestedMenuById(int id);
    }
}
