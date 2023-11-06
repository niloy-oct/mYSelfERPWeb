using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.Services;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Controllers
{
    public class NestedMenuController : CoreController
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        private readonly IModuleService _moduleService;
        public NestedMenuController(IMenuService menuService, IMapper mapper, IModuleService moduleService)
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
            var model = _menuService.GetAllNestedMenu();
            var vmodel = _mapper.Map<IEnumerable<NestedMenu>, IEnumerable<NestedMenuViewModel>>(model);
            return View(vmodel);
        }


        public ActionResult Create()
        {
            PopulateDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Create(NestedMenuViewModel nestedMenuViewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(nestedMenuViewModel.display_name) && (!string.IsNullOrEmpty(nestedMenuViewModel.form_location)))
            {
                var menuId = formCollection["menuId"];
                var submenuId = formCollection["submenuId"];
                var model = _mapper.Map<NestedMenuViewModel, NestedMenu>(nestedMenuViewModel);
                model.menu_id = int.Parse(menuId);
                model.sub_menu_id = int.Parse(submenuId);
                model.Module_Id = int.Parse(formCollection["moduleId"]);
                AddAuditTrail(model, true);
                _menuService.AddNestedMenu(model);
                _menuService.SaveNestedMenu();
                AddToastMessage("", "Nested Menu added successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                AddToastMessage("", "No Nested data not found to create", ToastType.Error);
                return RedirectToAction("Create");
            }

        }




        public ActionResult Edit(int id)
        {
            var model = _menuService.GetNestedMenuById(id);
            var vmodel = _mapper.Map<NestedMenu, NestedMenuViewModel>(model);
            PopulateDropDown();
            ViewBag.SelectedSubMenu = model.sub_menu_id.ToString();
            ViewBag.SelectedMenu = model.menu_id.ToString();
            ViewBag.SelectedModule = model.Module_Id.ToString();
            return View("Create", vmodel);
        }

        [HttpPost]
        public ActionResult Edit(NestedMenuViewModel nestedMenuViewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(nestedMenuViewModel.display_name) &&
                (!string.IsNullOrEmpty(nestedMenuViewModel.form_location)))
            {


                var model = _menuService.GetNestedMenuById(nestedMenuViewModel.Nested_menu_id);

                model.display_name = nestedMenuViewModel.display_name;
                model.form_location = nestedMenuViewModel.form_location;
                model.menu_id = int.Parse(formCollection["menuId"]);
                model.sub_menu_id = int.Parse(formCollection["submenuId"]);
                model.Module_Id = int.Parse(formCollection["moduleId"]);
                model.icon = nestedMenuViewModel.icon;
                AddAuditTrail(model, false);
                _menuService.UpdateNestedMenu(model);
                _menuService.SaveNestedMenu();
                AddToastMessage("", "Nested menu updated successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                AddToastMessage("", "No Nested menu data not found to edit", ToastType.Error);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult ChangeNestedMenuStatus(int id, string status)
        {
            var nestedMenu = _menuService.GetNestedMenuById(id);
            nestedMenu.Active = Convert.ToBoolean(status);
            AddAuditTrail(nestedMenu, false);
            _menuService.UpdateNestedMenu(nestedMenu);
            _menuService.SaveNestedMenu();
            AddToastMessage("", "NestedMenu status updated successfully.", ToastType.Success);
            return RedirectToAction("Index");
        }
    }
}