using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using mYSelfERPWeb.App_Start;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.Services;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Controllers
{
    public class RoleController : CoreController
    {

        private readonly DatabaseContext.DatabaseContext _databaseContext;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;

        public RoleController(DatabaseContext.DatabaseContext databaseContext, IRoleService roleService, IMapper mapper, IMenuService menuService)
        {
            _databaseContext = databaseContext;
            _roleService = roleService;
            _mapper = mapper;
            _menuService = menuService;

        }


        //[RoleBasedAuthorizationFilter("Admin")]
        public ActionResult Index()
        {
            var roles = _roleService.GetAllRoles();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View(new RoleViewModel());
        }


        [HttpPost]
        public ActionResult Create(RoleViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.RoleName))
            {
                var model = _mapper.Map<RoleViewModel, Role>(vmModel);
                model.Active = true;
                AddAuditTrail(model, true);
                _roleService.AddRole(model);
                _roleService.SaveRole();
                AddToastMessage("", "Role has been created successfully.", ToastType.Success);
                return RedirectToAction("Index");

            }
            else
            {
                AddToastMessage("", "Please enter role name create new role", ToastType.Error);
                return RedirectToAction("Create");
            }
        }


        public ActionResult Edit(int id)
        {
            var menuData = _menuService.GetAllMenuByUserRole(id);
            ViewBag.UserTypeId = id.ToString();
            return View(menuData);
        }

        [HttpPost]
        public ActionResult Edit(List<SelectedMenuData> selectedData, int userTypeId)
        {
            var userMenu = _menuService.GetUserMenuByUserTypeId(userTypeId);

            if (userMenu.Any())
            {
                var userMenuIds = userMenu.Select(x => x.RoleId).ToList();
                _menuService.DeleteUserMenuByUserTypeId(userMenuIds);
            }

            foreach (var data in selectedData)
            {
                var objUserMenu = new UserMenu();
                objUserMenu.RoleId = userTypeId;
                objUserMenu.menu_id = int.Parse(data.MenuId);
                objUserMenu.sub_menu_id = int.Parse(data.SubMenuId);
                objUserMenu.nested_menu_id = int.Parse(data.NestedMenuId);
                objUserMenu.Module_Id = int.Parse(data.ModuleId);
                AddAuditTrail(objUserMenu, true);
                _menuService.AddUserMenu(objUserMenu);
            }
            _menuService.SaveUserMenu();
            return Json(new { success = true, message = "Menu permission updated successfully", redirectToUrl = Url.Action("Index", "Role") });

        }
    }
}