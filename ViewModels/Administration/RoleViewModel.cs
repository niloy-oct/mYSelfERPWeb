using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        public string RoleName { get; set; }
        public bool Active { get; set; }
    }
}