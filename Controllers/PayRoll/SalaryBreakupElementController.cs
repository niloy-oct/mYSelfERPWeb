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
    public class SalaryBreakupElementController : CoreController
    {

        private readonly ISalaryBreakupElementService _salaryBreakupElementService;
        private readonly IMapper _mapper;
        private readonly IMiscellaneousService<SalaryBreakupElement> _miscellaneousService;

        public SalaryBreakupElementController(ISalaryBreakupElementService salaryBreakupElementService, IMapper mapper, IMiscellaneousService<SalaryBreakupElement> miscellaneousService)
        {
            _salaryBreakupElementService = salaryBreakupElementService;
            _mapper = mapper;
            _miscellaneousService = miscellaneousService;
        }


        public ActionResult Index()
        {
            var model = _salaryBreakupElementService.GetAllSalaryBreakupElements();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var code = _miscellaneousService.GetUniqueKey(i => int.Parse(i.Code));
            var model = new SalaryBreakupElementViewModel { Code = code };
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SalaryBreakupElementViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.Name))
            {
                var existingSalaryGroup = _miscellaneousService.GetDuplicateEntry(i => i.Name == vmModel.Name);
                if (existingSalaryGroup != null)
                {
                    AddToastMessage("", "A Salary breakup element with same name already exists in the system. Please try with a different name.", ToastType.Error);
                    return View(vmModel);
                }

                var model = _mapper.Map<SalaryBreakupElementViewModel, SalaryBreakupElement>(vmModel);
                model.CompanyCode = Sessions.Name.CompanyCode;
                model.SisterConcernCode = Sessions.Name.SisterConcernCode;
                model.Active = true;
                AddAuditTrail(model, true);
                _salaryBreakupElementService.AddSalaryBreakupElement(model);
                _salaryBreakupElementService.SaveSalaryBreakupElement();
                AddToastMessage("", "Salary Breakup Element Added successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            AddToastMessage("", "No data found to save salary breakup element", ToastType.Error);
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _salaryBreakupElementService.GetBreakupElementById(id);
            var vmodel = _mapper.Map<SalaryBreakupElement, SalaryBreakupElementViewModel>(model);
            return View("Create", vmodel);
        }

        [HttpPost]
        public ActionResult Edit(SalaryBreakupElementViewModel vmModel)
        {
            if (!string.IsNullOrEmpty(vmModel.Name))
            {
                var model = _salaryBreakupElementService.GetBreakupElementById(vmModel.Id);
                model.Name = vmModel.Name;
                model.Remarks = vmModel.Remarks;
                model.CompanyCode = Sessions.Name.CompanyCode;
                model.SisterConcernCode = Sessions.Name.SisterConcernCode;
                AddAuditTrail(model, false);
                _salaryBreakupElementService.UpdateSalaryBreakupElement(model);
                _salaryBreakupElementService.SaveSalaryBreakupElement();
                AddToastMessage("", "Salary Breakup Element updated successfully", ToastType.Success);
                return RedirectToAction("Index");
            }
            AddToastMessage("", "No data found to update Salary Breakup Element", ToastType.Error);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult ChangeStatus(int id, string status)
        {
            var salarybreakupelement = _salaryBreakupElementService.GetBreakupElementById(id);
            salarybreakupelement.Active = Convert.ToBoolean(status);
            AddAuditTrail(salarybreakupelement, false);
            _salaryBreakupElementService.UpdateSalaryBreakupElement(salarybreakupelement);
            _salaryBreakupElementService.SaveSalaryBreakupElement();
            AddToastMessage("", "Salary Breakup Element status updated successfully.", ToastType.Success);
            return RedirectToAction("Index");
        }
    }
}