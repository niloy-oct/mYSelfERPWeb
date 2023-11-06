using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public class RoleService : IRoleService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Role> _roleRepository;
        public RoleService(IBaseRepository<Role> roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddRole(Role role)
        {
            _roleRepository.Add(role);
        }

        public void UpdateRole(Role role)
        {
            _roleRepository.Update(role);
        }

        public void SaveRole()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _roleRepository.GetAllAsEnumerable();
        }

        public Role GetRoleById(int Id)
        {
            return _roleRepository.FindBy(i=>i.Id == Id).FirstOrDefault();
        }
    }
}