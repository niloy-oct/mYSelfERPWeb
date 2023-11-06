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
    public class UserController : CoreController
    {
        private readonly DatabaseContext.DatabaseContext _databaseContext;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly ISisterConcernService _sisterConcernService;
        


        public UserController(DatabaseContext.DatabaseContext databaseContext, IUserService userService, IRoleService roleService, IMapper mapper, ICompanyService companyService, ISisterConcernService sisterConcernService)
        {
            _databaseContext = databaseContext;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
            _companyService = companyService;
            _sisterConcernService = sisterConcernService;

        }

        private void PopulateDropDown()
        {
            var roles = _roleService.GetAllRoles().Where(i=>i.Active = true);
            ViewBag.Roles = roles.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.RoleName
            });


            var companies = _companyService.GetAllCompanies();
            ViewBag.Companies = companies.Select(m => new SelectListItem
            {
                Value = m.ComCode,
                Text = m.ComName
            });

            var concerns = _sisterConcernService.GetAllSisterConcerns();
            ViewBag.Concerns = concerns.Select(m => new SelectListItem
            {
                Value = m.SisterConcernCode,
                Text = m.SisterConcernName
            });

        }

        public ActionResult Index()
        {
            var users = _userService.GetUsers(Sessions.Name.Role);
            return View(users);
        }


        public ActionResult Create()
        {
            PopulateDropDown();
            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel, FormCollection formCollection)
        {
            PopulateDropDown();

            if (!string.IsNullOrWhiteSpace(userViewModel.UserID))
            {
                var model = _mapper.Map<UserViewModel, User>(userViewModel);
                model.CompanyCode = formCollection["companyCode"];
                model.SisterConcernCode = formCollection["concernCode"];
                var (hash, salt) = _userService.HashPassword("Test@123");
                model.SaltKey = salt;
                model.PasswordHash = hash;
                AddAuditTrail(model, true);
                _userService.AddUser(model);
                _userService.SaveUser();
                AddToastMessage("","New user created successfully with default password Test@123",ToastType.Success);
                return RedirectToAction("Index");

            }

            AddToastMessage("", "Please enter User ID create new user", ToastType.Error);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _userService.GetUserById(id);
            var vmodel = _mapper.Map<User, UserViewModel>(model);
            PopulateDropDown();
            ViewBag.SelectedRole = model.RoleID.ToString();
            return View("Create", vmodel);
        }


        [HttpPost]

        public ActionResult Edit(UserViewModel userViewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrWhiteSpace(userViewModel.UserID))
            {
                var model = _userService.GetUserById(userViewModel.Id);
                model.UserID = userViewModel.UserID;
                //model.Password = userViewModel.Password;
                model.RoleID = userViewModel.RoleID;
                model.CompanyCode = formCollection["companyCode"];
                model.SisterConcernCode = formCollection["concernCode"];
                AddAuditTrail(model,false);
                _userService.UpdateUser(model);
                _userService.SaveUser();
                AddToastMessage("", " User updated successfully", ToastType.Success);
                return RedirectToAction("Index");

            }
            AddToastMessage("", "Please enter user id and password to update user", ToastType.Error);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult ChangUserStatus(int id, string status)
        {
            var user = _userService.GetUserById(id);
            user.Active = Convert.ToBoolean(status);
            AddAuditTrail(user,false);
            _userService.UpdateUser(user);
            _userService.SaveUser();
            AddToastMessage("", "User status updated successfully.", ToastType.Success);
            return RedirectToAction("Index");
        }

    }
}