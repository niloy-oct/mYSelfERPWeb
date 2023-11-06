using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.Services;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Controllers
{
    public class CompanyController : CoreController
    {
        private static ICompanyService _companyService;
        private static IMapper _mapper;
        private static IMiscellaneousService<Company> _miscellaneousService;

        public CompanyController(ICompanyService companyService, IMapper mapper,
            IMiscellaneousService<Company> miscellaneousService)
        {
            _companyService = companyService;
            _mapper = mapper;
            _miscellaneousService = miscellaneousService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var model = _companyService.GetAllCompanies();
            var vmodel = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(model);
            return View(vmodel);
        }

        public ActionResult Create()
        {
            var companycode = _miscellaneousService.GetUniqueKey(i => int.Parse(i.ComCode));
            var model = new CompanyViewModel { ComCode = companycode };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.ComName))
            {
                var existingCompany = _miscellaneousService.GetDuplicateEntry(i => i.ComName == viewModel.ComName);
                if (existingCompany != null)
                {
                    AddToastMessage("", "A company with same name already exists in the system. Please try with a different name.", ToastType.Error);
                    return View(viewModel);
                }

                var model = _mapper.Map<CompanyViewModel, Company>(viewModel);
                HttpPostedFileBase file = Request.Files["companylogo"];
                model.ComLogo = ConvertToBytes(file);
                AddAuditTrail(model, true);

                _companyService.AddCompany(model);
                _companyService.SaveCompany();
                AddToastMessage("", "Company Save Successfully", ToastType.Success);
                return RedirectToAction("Index");

            }
            AddToastMessage("", "Please add company name to create new company");
            return View();
        }


        public ActionResult Edit(int id)
        {
            var model = _companyService.GetCompanyById(id);
            var vmodel = _mapper.Map<Company, CompanyViewModel>(model);
            var image = GetImageFromDataBase(vmodel.Id);
            if (image != null)
            {
                ViewBag.CompanyLogo = "data:image/jpg;base64," + Convert.ToBase64String(image);
            }
            return View("Create", vmodel);
        }


        [HttpPost]
        public ActionResult Edit(CompanyViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.ComName))
            {
                var model = _companyService.GetCompanyById(viewModel.Id);


                model.ComCode = viewModel.ComCode;
                model.ComName = viewModel.ComName;
                model.Address = viewModel.Address;
                model.Phone = viewModel.Phone;
                model.Fax = viewModel.Fax;
                model.Mobile = viewModel.Mobile;
                model.Email = viewModel.Email;
                model.WebAddress = viewModel.WebAddress;
                HttpPostedFileBase file = Request.Files["companylogo"];

                if (file.ContentLength > 0)
                {
                    model.ComLogo = ConvertToBytes(file);
                }

                AddAuditTrail(model,false);
                _companyService.UpdateCompany(model);
                _companyService.SaveCompany();
                AddToastMessage("", "Company Updated Successfully", ToastType.Success);
                return RedirectToAction("Index");

            }
            AddToastMessage("", "Please add company name to update company");
            return RedirectToAction("Index");
        }


        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            var company = _companyService.GetCompanyById(Id);
            return company.ComLogo;
        }


    }
}