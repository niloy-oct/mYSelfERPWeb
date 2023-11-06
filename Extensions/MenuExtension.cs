using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Extensions
{
    public static class MenuExtension
    {
        public static IEnumerable<UserMenuViewModel> GetAllMenuByUserRole(
            this IBaseRepository<SubMenu> subMenuRepository, IBaseRepository<Menu> menuRepository, IBaseRepository<NestedMenu> nestedMenuRepository,
            IBaseRepository<UserMenu> userMenuRepository, IBaseRepository<Module> moduleRepository, int roleId)
        {
            var result = from submenu in subMenuRepository.All
                join module in moduleRepository.All on submenu.Module_Id equals module.Module_id
                join menu in menuRepository.All on submenu.menu_id equals menu.Menu_id
                join nestedMenu in nestedMenuRepository.All on submenu.sub_menu_id equals nestedMenu.sub_menu_id
                orderby menu.Menu_id, submenu.sub_menu_id, nestedMenu.Nested_menu_id
                select new UserMenuViewModel
                {
                    user_type_id = roleId,
                    menu_id = submenu.menu_id,
                    menu_details = menu.Menu_name,
                    sub_menu_id = submenu.sub_menu_id,
                    display_name = submenu.display_name,
                    nested_menu_id = nestedMenu.Nested_menu_id,
                    nested_menu_name = nestedMenu.display_name,
                    module_id = module.Module_id,
                    module_name = module.Module_name,
                    is_permission = (userMenuRepository.All.Any(um =>
                        um.RoleId == roleId && um.sub_menu_id == submenu.sub_menu_id && um.menu_id == menu.Menu_id && um.Module_Id == module.Module_id && um.nested_menu_id == nestedMenu.Nested_menu_id)) ? "1" : "0"
                };



            return result.ToList();
        }
    }
}