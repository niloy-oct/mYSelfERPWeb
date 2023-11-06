using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mYSelfERPWeb.App_Start
{
    public class RoleBasedAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _requiredRole;

        public RoleBasedAuthorizationFilter(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            if (HttpContext.Current.Session["Name"] == null)
            {
               
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            
            string userRole = Sessions.Name.Role;
            if (!string.IsNullOrEmpty(userRole) && userRole.Equals(_requiredRole, StringComparison.OrdinalIgnoreCase))
            {
                
                return;
            }

           
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}