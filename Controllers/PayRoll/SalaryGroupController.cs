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
    public class SalaryGroupController : CoreController
    {
        private  readonly ISalaryGroupService _salaryGroupService;
        private readonly IMapper _mapper;
        private readonly IMiscellaneousService<SalaryGroup> _miscellaneousService;

        public SalaryGroupController(ISalaryGroupService salaryGroupService, IMapper mapper, IMiscellaneousService<SalaryGroup> miscellaneousService)
        {
            _salaryGroupService = salaryGroupService;
            _mapper = mapper;
            _miscellaneousService = miscellaneousService;
        }

        public ActionResult Index()
        {
            var model = _salaryGroupService.GetAllSalaryGroups();
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var code = _miscellaneousService.GetUniqueKey(i => int.Parse(i.Code));
            var model = new SalaryGroupViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SalaryGroupViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.Name))
            {
                var existingSalaryGroup = _miscellaneousService.GetDuplicateEntry(i => i.Name == vmModel.Name);
                if (existingSalaryGroup != null)
                {
                    AddToastMessage("", "A Salary group with same name already exists in the system. Please try with a different name.", ToastType.Error);
                    return View(vmModel);
                }

                var model = _mapper.Map<SalaryGroupViewModel, SalaryGroup>(vmModel);
                model.CompanyCode = Sessions.Name.CompanyCode;
                model.SisterConcernCode = Sessions.Name.SisterConcernCode;
                model.Active = true;
                AddAuditTrail(model,true);
                _salaryGroupService.AddSalaryGroup(model);
                _salaryGroupService.SaveSalaryGroup();
                AddToastMessage("","Salary Group Added successfully",ToastType.Success);
                return RedirectToAction("Index");
            }
            AddToastMessage("","No data found to save salary group",ToastType.Error);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _salaryGroupService.GetSalaryGroupById(id);
            var vmodel = _mapper.Map<SalaryGroup, SalaryGroupViewModel>(model);
            return View("Create", vmodel);
        }

        [HttpPost]

        public ActionResult Edit(SalaryGroupViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.Name))
            {
                var model = _salaryGroupService.GetSalaryGroupById(vmModel.Id);
                model.Name = vmModel.Name;
                model.Remarks = vmModel.Remarks;
                model.CompanyCode = Sessions.Name.CompanyCode;
                model.SisterConcernCode = Sessions.Name.SisterConcernCode;
                AddAuditTrail(model, false);
                _salaryGroupService.UpdateSalaryGroup(model);
                _salaryGroupService.SaveSalaryGroup();
                AddToastMessage("", "Salary Group updated successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            AddToastMessage("", "No data found to update salary group", ToastType.Error);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult ChangeSalaryGroupStatus(int id, string status)
        {
            var salaryGroup = _salaryGroupService.GetSalaryGroupById(id);
            salaryGroup.Active = Convert.ToBoolean(status);
            AddAuditTrail(salaryGroup, false);
            _salaryGroupService.UpdateSalaryGroup(salaryGroup);
            _salaryGroupService.SaveSalaryGroup();
            AddToastMessage("", "Salary group status updated successfully.", ToastType.Success);
            return RedirectToAction("Index");
        }
    }
}