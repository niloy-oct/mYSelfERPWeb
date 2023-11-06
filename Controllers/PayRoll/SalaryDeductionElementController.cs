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
    public class SalaryDeductionElementController : CoreController
    {
        private readonly ISalaryDeductionElementService _salaryDeductionElementService;
        private readonly IMapper _mapper;
        private readonly IMiscellaneousService<SalaryDeductionElement> _miscellaneousService;

        public SalaryDeductionElementController(ISalaryDeductionElementService salaryDeductionElementService, IMapper mapper, IMiscellaneousService<SalaryDeductionElement> miscellaneousService)
        {
            _salaryDeductionElementService = salaryDeductionElementService;
            _mapper = mapper;
            _miscellaneousService = miscellaneousService;
        }
        public ActionResult Index()
        {
            var model = _salaryDeductionElementService.GetAllsalaryDeductionElements();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var code = _miscellaneousService.GetUniqueKey(i => int.Parse(i.Code));
            var model = new SalaryDeductionElementViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SalaryDeductionElementViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.Name))
            {
                var existingSalaryGroup = _miscellaneousService.GetDuplicateEntry(i => i.Name == vmModel.Name);
                if (existingSalaryGroup != null)
                {
                    AddToastMessage("", "A Salary deduction element with same name already exists in the system. Please try with a different name.", ToastType.Error);
                    return View(vmModel);
                }

                var model = _mapper.Map<SalaryDeductionElementViewModel, SalaryDeductionElement>(vmModel);
                model.CompanyCode = Sessions.Name.CompanyCode;
                model.SisterConcernCode = Sessions.Name.SisterConcernCode;
                model.Active = true;
                AddAuditTrail(model, true);
                _salaryDeductionElementService.AddSalaryDeductionElement(model);
                _salaryDeductionElementService.SaveSalaryDeductionElement();
                AddToastMessage("", "Salary Deduction Element Added successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            AddToastMessage("", "No data found to save salary deduction element", ToastType.Error);
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _salaryDeductionElementService.GetSalaryDeductionElementById(id);
            var vmodel = _mapper.Map<SalaryDeductionElement, SalaryDeductionElementViewModel>(model);
            return View("Create", vmodel);
        }


        [HttpPost]
        public ActionResult Edit(SalaryDeductionElementViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.Name))
            {
                var model = _salaryDeductionElementService.GetSalaryDeductionElementById(vmModel.Id);
                model.Name = vmModel.Name;
                model.Remarks = vmModel.Remarks;
                model.CompanyCode = Sessions.Name.CompanyCode;
                model.SisterConcernCode = Sessions.Name.SisterConcernCode;
                AddAuditTrail(model, false);
                _salaryDeductionElementService.UpdateSalaryDeductionElement(model);
                _salaryDeductionElementService.SaveSalaryDeductionElement();
                AddToastMessage("", "Salary Deduction Element updated successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            AddToastMessage("", "No data found to update Salary deduction Element", ToastType.Error);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChangeStatus(int id, string status)
        {
            var salaryDeducctionelement = _salaryDeductionElementService.GetSalaryDeductionElementById(id);
            salaryDeducctionelement.Active = Convert.ToBoolean(status);
            AddAuditTrail(salaryDeducctionelement, false);
            _salaryDeductionElementService.UpdateSalaryDeductionElement(salaryDeducctionelement);
            _salaryDeductionElementService.SaveSalaryDeductionElement();
            AddToastMessage("", "Salary Deduction Element status updated successfully.", ToastType.Success);
            return RedirectToAction("Index");
        }
    }
}