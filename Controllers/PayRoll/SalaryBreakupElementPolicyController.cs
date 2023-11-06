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
    public class SalaryBreakupElementPolicyController : CoreController
    {
        private readonly ISalaryBreakupElementService _salaryBreakupElementService;
        private readonly ISalaryGroupService _salaryGroupService;
        private readonly ISalaryBreakupElementPolicyService _salaryBreakupElementPolicyService;
        private readonly IMapper _mapper;

        public SalaryBreakupElementPolicyController(ISalaryBreakupElementService salaryBreakupElementService,
            ISalaryGroupService salaryGroupService, ISalaryBreakupElementPolicyService salaryBreakupElementPolicyService, IMapper mapper)
        {
            _salaryBreakupElementService = salaryBreakupElementService;
            _salaryGroupService = salaryGroupService;
            _salaryBreakupElementPolicyService = salaryBreakupElementPolicyService;
            _mapper = mapper;
        }

        public void PopulateDropDowns()
        {
            var salarygroups = _salaryGroupService.GetAllSalaryGroups();
            ViewBag.SalaryGroup = salarygroups.Select(sg => new SelectListItem
            {
                Value = sg.Code,
                Text = sg.Name
            });

            var breakupelements = _salaryBreakupElementService.GetAllSalaryBreakupElements();
            ViewBag.BreakupElement = breakupelements.Select(be => new SelectListItem
            {
                Value = be.Code,
                Text = be.Name
            });
        }
        public ActionResult Index()
        {
            var data = _salaryBreakupElementPolicyService.GetDistinctSalaryGroups();
            return View(data);
        }

        public ActionResult Create()
        {
            PopulateDropDowns();
            return View();
        }

        [HttpPost]
        public ActionResult Create(string TableData, string SalaryGroupValue)
        {
            if (ModelState.IsValid)
            {
                List<SalaryElement> salaryElements = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SalaryElement>>(TableData);

                if (salaryElements != null && salaryElements.Any())
                {
                   
                    var master = new SalaryBreakupElementPolicy
                    {
                      
                        CompanyCode = Sessions.Name.CompanyCode,
                        SisterConcernCode = Sessions.Name.SisterConcernCode,
                        salary_group_code = SalaryGroupValue, 
                        grade_lavel_code = "",
                        IsActive = true,
                        CreatedBy = Sessions.Name.UserId,
                        CreationDate = GetLocalDateTime()
                    };

                    _salaryBreakupElementPolicyService.AddBreakupElementPolicy(master);
                    _salaryBreakupElementPolicyService.SaveBreakupElementPolicy();

                  
                    var details = salaryElements.Select(element => new SalaryBreakupElementPolicyDetails
                    {
                        CompanyCode = Sessions.Name.CompanyCode,
                        SisterConcernCode = Sessions.Name.SisterConcernCode,
                        SalaryGroupCode = SalaryGroupValue,
                        BreakupElementCode = element.ElementCode,
                        Amount = Convert.ToDecimal(element.Amount)
                    }).ToList();

                    _salaryBreakupElementPolicyService.AddBreakupElementDetailsPolicy(details);
                    _salaryBreakupElementPolicyService.SaveBreakupElementDetailsPolicy();
                }
                AddToastMessage("","Salary Breakup Element Policy Saved Successfully",ToastType.Success);
                return RedirectToAction("Index");
            }

            AddToastMessage("", "Salary Breakup Element Policy not saved ", ToastType.Error);
            return View();
        }


        public ActionResult Edit(string salarygroup)
        {
            var sbepolicy = _salaryBreakupElementPolicyService.GetBreakupElementPolicyBySalaryGroup(salarygroup);

            var sbepolicydetails = _salaryBreakupElementPolicyService.GetBreakupElementPolicyDetailsList(salarygroup);

            PopulateDropDowns();

            var viewModel = new SalaryBreakupPolicyViewModel
            {
                Master = sbepolicy,
                PolicyDetails = sbepolicydetails
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(string TableData, string SalaryGroupValue)
        {

            _salaryBreakupElementPolicyService.DeleteBreakupElementPolicy(SalaryGroupValue);
            _salaryBreakupElementPolicyService.DeleteBreakupElementDeatilsPolicy(SalaryGroupValue);

            List<SalaryElement> salaryElements = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SalaryElement>>(TableData);

            if (salaryElements != null && salaryElements.Any())
            {
              
                var master = new SalaryBreakupElementPolicy
                {
                   
                    CompanyCode = Sessions.Name.CompanyCode,
                    SisterConcernCode = Sessions.Name.SisterConcernCode,
                    salary_group_code = SalaryGroupValue,
                    grade_lavel_code = "",
                    IsActive = true,
                    CreatedBy = Sessions.Name.UserId,
                    CreationDate = GetLocalDateTime()
                };

                _salaryBreakupElementPolicyService.AddBreakupElementPolicy(master);
                _salaryBreakupElementPolicyService.SaveBreakupElementPolicy();


                var details = salaryElements.Select(element => new SalaryBreakupElementPolicyDetails
                {
                    CompanyCode = Sessions.Name.CompanyCode,
                    SisterConcernCode = Sessions.Name.SisterConcernCode,
                    SalaryGroupCode = SalaryGroupValue,
                    BreakupElementCode = element.ElementCode,
                    Amount = Convert.ToDecimal(element.Amount)
                }).ToList();

                _salaryBreakupElementPolicyService.AddBreakupElementDetailsPolicy(details);
                _salaryBreakupElementPolicyService.SaveBreakupElementDetailsPolicy();
            }
            AddToastMessage("", "Salary Breakup Element Policy Updated Successfully", ToastType.Success);
            return RedirectToAction("Index");
            
        }

    }
}