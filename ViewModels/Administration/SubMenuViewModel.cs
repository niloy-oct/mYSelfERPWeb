using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.ViewModels
{
    public class SubMenuViewModel
    {
        public int sub_menu_id { get; set; }
        public int menu_id { get; set; }
        public int Module_Id { get; set; }
        public string form_location { get; set; }
        public string display_name { get; set; }
        public string icon { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }
    }
}