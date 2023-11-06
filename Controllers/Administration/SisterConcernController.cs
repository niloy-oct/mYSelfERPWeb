using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.Services;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Controllers
{
    public class SisterConcernController : CoreController
    {

        private static ISisterConcernService _sisterConcernService;
        private static IMapper _mapper;
        private static IMiscellaneousService<SisterConcern> _miscellaneousService;
        private static ICompanyService _companyService;


        public SisterConcernController(ISisterConcernService sisterConcernService, IMapper mapper,
            IMiscellaneousService<SisterConcern> miscellaneousService, ICompanyService companyService)
        {
            _sisterConcernService = sisterConcernService;
            _mapper = mapper;
            _miscellaneousService = miscellaneousService;
            _companyService = companyService;
        }



        private void PopulateDropDown()
        {
            var companies = _companyService.GetAllCompanies();
            ViewBag.Companies = companies.Select(m => new SelectListItem
            {
                Value = m.ComCode,
                Text = m.ComName
            });
        }

        public ActionResult Index()
        {

            var model = _sisterConcernService.GetAllSisterConcernsWithCompanyName(Sessions.Name.Role);
            return View(model);
            
        }


        public ActionResult Create()
        {
            PopulateDropDown();
            var sisterConcernCode = _miscellaneousService.GetConcernUniqueKey(i => int.Parse(i.SisterConcernCode));
            var model = new SisterConcernViewModel { SisterConcernCode = sisterConcernCode };
            return View(model);
        }



        [HttpPost]
        public ActionResult Create(SisterConcernViewModel viewModel,FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(viewModel.SisterConcernName))
            {
                var existingConcern = _miscellaneousService.GetDuplicateEntry(i => i.SisterConcernName == viewModel.SisterConcernName);
                if (existingConcern != null)
                {
                    AddToastMessage("", "A sister concern with same name already exists in the system. Please try with a different name.", ToastType.Error);
                    return View(viewModel);
                }

                var model = _mapper.Map<SisterConcernViewModel, SisterConcern>(viewModel);
                HttpPostedFileBase file = Request.Files["concernlogo"];
                model.ComLogo = ConvertToBytes(file);
                model.ComCode = formCollection["company"].ToString();
                AddAuditTrail(model, true);

                _sisterConcernService.AddSisterConcern(model);
                _sisterConcernService.SaveSisterConcern();
                AddToastMessage("", "Sister Concern Save Successfully", ToastType.Success);
                return RedirectToAction("Index");

            }
            AddToastMessage("", "Please add concern name to create new company");
            PopulateDropDown();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = _sisterConcernService.GetSisterConcernById(id);
            var vmodel = _mapper.Map<SisterConcern, SisterConcernViewModel>(model);
            var image = GetImageFromDataBase(vmodel.Id);
            if (image != null)
            {
                ViewBag.SisterConcernLogo = "data:image/jpg;base64," + Convert.ToBase64String(image);
            }
            ViewBag.SelectedCompany = model.ComCode;
            PopulateDropDown();
            return View("Create", vmodel);
        }


        [HttpPost]
        public ActionResult Edit(SisterConcernViewModel viewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(viewModel.SisterConcernName))
            {
                var model = _sisterConcernService.GetSisterConcernById(viewModel.Id);

                model.ComCode = formCollection["company"];
                model.SisterConcernCode = viewModel.SisterConcernCode;
                model.SisterConcernName = viewModel.SisterConcernName;
                model.Address = viewModel.Address;
                model.Phone = viewModel.Phone;
                model.Fax = viewModel.Fax;
                model.Mobile = viewModel.Mobile;
                model.Email = viewModel.Email;
                model.WebAddress = viewModel.WebAddress;
                HttpPostedFileBase file = Request.Files["concernlogo"];
                if (file.ContentLength > 0)
                {
                    model.ComLogo = ConvertToBytes(file);
                }
                AddAuditTrail(model, false);
                _sisterConcernService.UpdateSisterConcern(model);
                _sisterConcernService.SaveSisterConcern();
                AddToastMessage("", "Sister Concern Updated Successfully", ToastType.Success);
                return RedirectToAction("Index");

            }
            AddToastMessage("", "Please add sister concern name to update sister concern");
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
            var concern = _sisterConcernService.GetSisterConcernById(Id);
            return concern.ComLogo;
        }


    }
}