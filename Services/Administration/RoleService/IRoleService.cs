using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public interface IRoleService
    {
        void AddRole(Role role);
        void UpdateRole(Role role);
        void SaveRole();
        IEnumerable<Role> GetAllRoles();

        Role GetRoleById(int Id);

    }
}
