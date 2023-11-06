using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using mYSelfERPWeb.Services;

namespace mYSelfERPWeb.Controllers
{
    public class AccountController : CoreController
    {

        private readonly DatabaseContext.DatabaseContext _databaseContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly ISMSService _smsService;


        public AccountController(IUserService userService, DatabaseContext.DatabaseContext databaseContext, IMapper mapper, IRoleService roleService, ISMSService smsService)
        {
            _userService = userService;
            _databaseContext = databaseContext;
            _mapper = mapper;
            _roleService = roleService;
            _smsService = smsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public async Task<ActionResult> Login(FormCollection formCollection)
        {
            var userId = formCollection["userId"];
            var password = formCollection["password"];

            var action = formCollection["action"];

            if (action == "login")
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    var userInfo = _userService.GetLoginDetailsByUserID(userId).FirstOrDefault();

                    bool VarifyPassword = _userService.VerifyPassword(password, userInfo.SaltKey, userInfo.PasswordHash);

                    if (VarifyPassword)
                    {
                        if (userInfo != null && userInfo.Active == true)
                        {
                            if (Sessions.Name == null)
                            {
                                Sessions.Name = new SessionInfo();
                            }

                            Sessions.Name.UserId = userInfo.UserID;
                            Sessions.Name.UserName = userInfo.UserID;
                            Sessions.Name.RoleId = userInfo.RoleID;
                            Sessions.Name.Role = userInfo.RoleName;
                            Sessions.Name.CompanyName = userInfo.CompanyName;
                            Sessions.Name.SisterConcernName = userInfo.SisterConcernName;

                            Sessions.Name.CompanyCode = userInfo.CompanyCode;
                            Sessions.Name.SisterConcernCode = userInfo.SisterConcernCode;
                            Sessions.Name.CompanyAddress = userInfo.CompanyAddress;
                            Sessions.Name.SisterConcernAddress = userInfo.SisterConcernAddress;
                            Sessions.Name.CompanyLogo = userInfo.CompanyLogo;
                            Sessions.Name.SisterConcernLogo = userInfo.SisterConcernLogo;


                            AddToastMessage($"Welcome {Sessions.Name.UserName}", "Login Successfully", ToastType.Success);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            AddToastMessage("", "User currently inactive", ToastType.Warning);
                            return View();
                        }
                    }
                }
                AddToastMessage("", "Invalid user id password", ToastType.Error);
                return View();

            }

            else
            {
                var recoveruserId = formCollection["recoveryUserID"];
                var mobileNo = formCollection["mobileNo"];

                var userInfo = _userService.GetUserByUserId(recoveruserId);

                if (userInfo == null)
                {
                    AddToastMessage("", "Invalid User ID", ToastType.Error);
                    return View();
                }
                var resetPassword = GeneratePassword();
                var (hash, salt) = _userService.HashPassword(resetPassword);
                userInfo.SaltKey = salt;
                userInfo.PasswordHash = hash;
                _userService.UpdateUser(userInfo);
                _userService.SaveUser();
                string smsText = $"Your new password is: {resetPassword}. Please use this password to log in to your account. For security reasons, we recommend changing your password after logging in.";
                string response = await _smsService.SendSmsAsync(mobileNo, smsText);
                AddToastMessage("", "New password send via sms, check message inbox.", ToastType.Success);
                return View();
            }

            return View();
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                AddToastMessage("", "Please fill in all fields.", ToastType.Error);
                return View();
            }

            var userId = Sessions.Name.UserId; 

            var userInfo = _userService.GetUserByUserId(userId);

            if (userInfo != null)
            {
                bool isPasswordCorrect = _userService.VerifyPassword(oldPassword, userInfo.SaltKey, userInfo.PasswordHash);

                if (isPasswordCorrect)
                {
                    if (newPassword == confirmPassword)
                    {
                        var (hash, salt) = _userService.HashPassword(newPassword);
                        userInfo.SaltKey = salt;
                        userInfo.PasswordHash = hash;

                        _userService.UpdateUser(userInfo);
                        _userService.SaveUser();
                        AddToastMessage("", "Password has been changed successfully,now login with new password", ToastType.Success);
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        AddToastMessage("", "New password and confirmation do not match.", ToastType.Error);
                    }
                }
                else
                {
                    AddToastMessage("", "Incorrect old password.", ToastType.Error);
                }
            }

            return View();
        }


    }
}