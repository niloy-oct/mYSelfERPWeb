using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Extensions;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuRepository _menuRepository;
        private readonly IBaseRepository<SubMenu> _subMenuRepository;
        private readonly IBaseRepository<UserMenu> _userMenuRepository;
        private readonly IBaseRepository<Menu> _menuBaseRepository;
        private readonly IBaseRepository<NestedMenu> _nestedRepository;
        private readonly IBaseRepository<Module> _moduleRepository;

        public MenuService(IUnitOfWork unitOfWork, IMenuRepository menuRepository, IBaseRepository<SubMenu> subMenuRepository,
            IBaseRepository<UserMenu> userMenuRepository, IBaseRepository<Menu> menuBaseRepository, IBaseRepository<NestedMenu> nestedRepository, IBaseRepository<Module> moduleRepository)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = menuRepository;
            _subMenuRepository = subMenuRepository;
            _userMenuRepository = userMenuRepository;
            _menuBaseRepository = menuBaseRepository;
            _nestedRepository = nestedRepository;
            _moduleRepository = moduleRepository;
        }

        public List<Dictionary<string, object>> GetMenuList()
        {
            return _menuRepository.GetMenuList();
        }

        public List<Dictionary<string, object>> GetSubMenuList()
        {
            return _menuRepository.GetSubMenuList();
        }

        public List<Dictionary<string, object>> GetNestedMenuList()
        {
            return _menuRepository.GetNestedMenuList();
        }

        public List<Dictionary<string, object>> GetModuleList()
        {
            return _menuRepository.GetModuleList();
        }


        public void AddUserMenu(UserMenu userMenus)
        {
            _userMenuRepository.Add(userMenus);
        }

        public void SaveUserMenu()
        {
            _unitOfWork.Commit();
        }

        public void DeleteUserMenuByUserTypeId(List<int> ids)
        {
            _userMenuRepository.Delete(x => ids.Contains(x.RoleId));
        }

        public List<UserMenu> GetUserMenuByUserTypeId(int id)
        {
            return _userMenuRepository.FindBy(x => x.RoleId == id).ToList();
        }

        public void AddMenu(Menu menu)
        {
            _menuBaseRepository.Add(menu);
        }

        public void UpdateMenu(Menu menu)
        {
            _menuBaseRepository.Update(menu);
        }

        public void SaveMenu()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Menu> GetAllMenu()
        {
            return _menuBaseRepository.GetAllAsEnumerable();
        }


        public void AddSubMenu(SubMenu subMenu)
        {
            _subMenuRepository.Add(subMenu);
        }

        public void UpdateSubMenu(SubMenu subMenu)
        {
            _subMenuRepository.Update(subMenu);
        }

        public void SaveSubMenu()
        {
            _unitOfWork.Commit();
        }


        public void AddNestedMenu(NestedMenu nestedMenu)
        {
            _nestedRepository.Add(nestedMenu);
        }

        public void UpdateNestedMenu(NestedMenu nestedMenu)
        {
            _nestedRepository.Update(nestedMenu);
        }

        public void SaveNestedMenu()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<SubMenu> GetAllSubMenu()
        {
            return _subMenuRepository.GetAllAsEnumerable();
        }

        public IEnumerable<NestedMenu> GetAllNestedMenu()
        {
            return _nestedRepository.GetAllAsEnumerable();
        }

        public Menu GetMenuById(int id)
        {
            return _menuBaseRepository.FindBy(x => x.Menu_id == id).FirstOrDefault();
        }

        public SubMenu GetSubMenuById(int id)
        {
            return _subMenuRepository.FindBy(x => x.sub_menu_id == id).FirstOrDefault();
        }

        public IEnumerable<UserMenuViewModel> GetAllMenuByUserRole(int roleId)
        {
            return _subMenuRepository.GetAllMenuByUserRole(_menuBaseRepository,  _nestedRepository,_userMenuRepository,_moduleRepository, roleId);
        }
        public NestedMenu GetNestedMenuById(int id)
        {
            return _nestedRepository.FindBy(x => x.Nested_menu_id == id).FirstOrDefault();
        }

    }
}