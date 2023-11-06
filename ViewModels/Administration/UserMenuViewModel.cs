using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.ViewModels
{

    public class UserMenuViewModel
    {
        public string submenu_name { get; set; }
        public string nested_menu_name { get; set; }
        public int menu_id { get; set; }
        public int sub_menu_id { get; set; }
        public int user_type_id { get; set; }
        public int nested_menu_id { get; set; }
        public int module_id { get; set; }
        public string menu_details { get; set; }
        public string display_name { get; set; }
        public string is_permission { get; set; }
        public string module_name { get; set; }
    }

    public class SelectedMenuData
    {
        public string MenuId { get; set; }
        public string SubMenuId { get; set; }
        public string NestedMenuId { get; set; }
        public string ModuleId { get; set; }
    }
}
