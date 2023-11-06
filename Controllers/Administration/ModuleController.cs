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
    public class ModuleController : CoreController
    {

        private readonly IModuleService _moduleService;
        private readonly IMapper _mapper;

        public ModuleController(IModuleService moduleService, IMapper mapper)
        {
            _moduleService = moduleService;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var modules = _moduleService.GetAllModules();
            return View(modules);
        }

        public ActionResult Create()
        {
            return View(new ModuleViewModel());
        }


        [HttpPost]
        public ActionResult Create(ModuleViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.Module_name))
            {
                var model = _mapper.Map<ModuleViewModel, Module>(vmModel);
                model.Active = true;
                AddAuditTrail(model, true);
                _moduleService.AddModule(model);
                _moduleService.SaveModule();
                AddToastMessage("", "Module has been created successfully.", ToastType.Success);
                return RedirectToAction("Index");

            }
            else
            {
                AddToastMessage("", "Please enter module name  to create new module", ToastType.Error);
                return RedirectToAction("Create");
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _moduleService.GetModuleById(id);
            var vmodel = _mapper.Map<Module, ModuleViewModel>(model);
            return View("Create",vmodel);
        }


        [HttpPost]
        public ActionResult Edit(ModuleViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.Module_name))
            {
                var model = _moduleService.GetModuleById(vmModel.Module_id);
                model.Module_name = vmModel.Module_name;
                AddAuditTrail(model, false);
                _moduleService.UpdateModule(model);
                _moduleService.SaveModule();
                AddToastMessage("", "Module has been updated successfully.", ToastType.Success);
                return RedirectToAction("Index");

            }
            else
            {
                AddToastMessage("", "Please enter module name to update module", ToastType.Error);
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult ChangeModuleStatus(int id, string status)
        {
            var module = _moduleService.GetModuleById(id);
            module.Active = Convert.ToBoolean(status);
            AddAuditTrail(module, false);
            _moduleService.UpdateModule(module);
            _moduleService.SaveModule();
            AddToastMessage("", "Module status updated successfully.", ToastType.Success);
            return RedirectToAction("Index");
        }
    }
}