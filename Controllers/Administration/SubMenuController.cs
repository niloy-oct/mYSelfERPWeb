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
    public class SubMenuController : CoreController
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        private readonly IModuleService _moduleService;
        public SubMenuController(IMenuService menuService, IMapper mapper, IModuleService moduleService)
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
        public ActionResult Index()
        {
            var model = _menuService.GetAllSubMenu();
            var vmodel = _mapper.Map<IEnumerable<SubMenu>, IEnumerable<SubMenuViewModel>>(model);
            return View(vmodel);
        }


        public ActionResult Create()
        {
            PopulateDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubMenuViewModel subMenuViewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(subMenuViewModel.display_name) && (!string.IsNullOrEmpty(subMenuViewModel.form_location)))
            {
                var menuId = formCollection["menuId"];
                var model = _mapper.Map<SubMenuViewModel, SubMenu>(subMenuViewModel);
                model.menu_id = int.Parse(menuId);
                model.Module_Id = int.Parse(formCollection["moduleId"]);
                AddAuditTrail(model, true);
                _menuService.AddSubMenu(model);
                _menuService.SaveSubMenu();
                AddToastMessage("", "Sub Menu added successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                AddToastMessage("", "No SubMenu data not found to create", ToastType.Error);
                return RedirectToAction("Create");
            }

        }
        [AuthorizationFilter]
        public ActionResult Edit(int id)
        {
            var model = _menuService.GetSubMenuById(id);
            var vmodel = _mapper.Map<SubMenu, SubMenuViewModel>(model);
            PopulateDropDown();
            ViewBag.SelectedMenu = model.menu_id.ToString();
            ViewBag.SelectedModule = model.Module_Id.ToString();
            return View("Create", vmodel);
        }

        [HttpPost]
        public ActionResult Edit(SubMenuViewModel subMenuViewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(subMenuViewModel.display_name) &&
                (!string.IsNullOrEmpty(subMenuViewModel.form_location)))
            {


                var model = _menuService.GetSubMenuById(subMenuViewModel.sub_menu_id);

                model.display_name = subMenuViewModel.display_name;
                model.form_location = subMenuViewModel.form_location;
                model.menu_id = int.Parse(formCollection["menuId"]);
                model.Module_Id = int.Parse(formCollection["moduleId"]);
                model.icon = subMenuViewModel.icon;
                AddAuditTrail(model, false);
                _menuService.UpdateSubMenu(model);
                _menuService.SaveSubMenu();
                AddToastMessage("", "Sub menu updated successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                AddToastMessage("", "No SubMenu data not found to edit", ToastType.Error);
                return RedirectToAction("Index");
            }
        }



        [HttpGet]
        public ActionResult ChangeSubMenuStatus(int id, string status)
        {
            var subMenu = _menuService.GetSubMenuById(id);
            subMenu.Active = Convert.ToBoolean(status);
            AddAuditTrail(subMenu, false);
            _menuService.UpdateSubMenu(subMenu);
            _menuService.SaveSubMenu();
            AddToastMessage("", "SubMenu status updated successfully.", ToastType.Success);
            return RedirectToAction("Index");
        }
    }
}