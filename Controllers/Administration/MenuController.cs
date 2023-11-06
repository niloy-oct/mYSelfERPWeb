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
    public class MenuController : CoreController
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        private readonly IModuleService _moduleService;
        public MenuController(IMenuService menuService, IMapper mapper, IModuleService moduleService)
        {
            _menuService = menuService;
            _mapper = mapper;
            _moduleService = moduleService;
        }


        private void PopulateDropDown()
        {
            var menus = _menuService.GetAllMenu().Where(i => i.Active = true);
            ViewBag.Menus = menus.Select(m => new SelectListItem
            {
                Value = m.Menu_id.ToString(),
                Text = m.Menu_name
            });


            var submenus = _menuService.GetAllSubMenu().Where(i => i.Active = true);
            ViewBag.SubMenus = submenus.Select(m => new SelectListItem
            {
                Value = m.sub_menu_id.ToString(),
                Text = m.display_name
            });

            var modules = _moduleService.GetAllModules().Where(i => i.Active = true);
            ViewBag.Modules = modules.Select(m => new SelectListItem
            {
                Value = m.Module_id.ToString(),
                Text = m.Module_name
            });
        }

        public ActionResult MenuNavigation()
        {
            ViewBag.MenueList = _menuService.GetMenuList();
            ViewBag.SubMenueList = _menuService.GetSubMenuList();
            ViewBag.NestedMenuList = _menuService.GetNestedMenuList();
            ViewBag.ModuleList = _menuService.GetModuleList();
            return PartialView();
        }

        public ActionResult Index()
        {
            var model = _menuService.GetAllMenu();
            var vmodel = _mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(model);
            return View(vmodel);
        }

        public ActionResult Create()
        {
            PopulateDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Create(MenuViewModel menuViewModel, FormCollection formCollection)
        {
            PopulateDropDown();
            if (!string.IsNullOrEmpty(menuViewModel.Menu_name))
            {
                var model = _mapper.Map<MenuViewModel, Menu>(menuViewModel);
                AddAuditTrail(model, true);
                model.Module_Id = int.Parse(formCollection["moduleId"]);
                _menuService.AddMenu(model);
                _menuService.SaveMenu();
                AddToastMessage("", "Menu added successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                AddToastMessage("", "No Menu data not found to create", ToastType.Error);
                return RedirectToAction("Create");
            }
        }


        [AuthorizationFilter]
        public ActionResult Edit(int id)
        {
            var model = _menuService.GetMenuById(id);
            var vmodel = _mapper.Map<Menu, MenuViewModel>(model);
            ViewBag.SelectedModule = model.Module_Id.ToString();
            PopulateDropDown();
            return View("Create", vmodel);
        }

        [HttpPost]
        public ActionResult Edit(MenuViewModel menuViewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(menuViewModel.Menu_name))
            {

                var model = _menuService.GetMenuById(menuViewModel.Menu_id);

                model.Menu_name = menuViewModel.Menu_name;
                model.Icon = menuViewModel.Icon;
                model.Module_Id = int.Parse(formCollection["moduleId"]);
                AddAuditTrail(model, false);
                _menuService.UpdateMenu(model);
                _menuService.SaveMenu();
                AddToastMessage("", "Menu updated successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                AddToastMessage("", "No Menu data not found to edit", ToastType.Error);
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ActionResult ChangeMenuStatus(int id, string status)
        {
            var menu = _menuService.GetMenuById(id);
            menu.Active = Convert.ToBoolean(status);
            AddAuditTrail(menu, false);
            _menuService.UpdateMenu(menu);
            _menuService.SaveMenu();
            AddToastMessage("", "Menu status updated successfully.", ToastType.Success);
            return RedirectToAction("Index");
        }

    }
}