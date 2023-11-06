using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace mYSelfERPWeb
{
    public static class HtmlBreadCrumbExtension
    {
        public static MvcHtmlString BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            if (helper.ViewContext.RouteData.Values["controller"].ToString() == "Home" ||
                (helper.ViewContext.RouteData.Values["controller"].ToString() == "Account" &&
                 helper.ViewContext.RouteData.Values["action"].ToString() == "Login"))
            {
                return MvcHtmlString.Empty; // Return an empty string if not needed
            }

            var breadcrumb = new StringBuilder("<ol class='breadcrumb border border-light-info px-3 rounded'>");
            breadcrumb.Append("<li class='breadcrumb-item'>");
            breadcrumb.Append(helper.ActionLink("Home", "Index", "Home").ToHtmlString());
            breadcrumb.Append("</li>");

            string controllerName = helper.ViewContext.RouteData.Values["controller"].ToString();
            string actionName = helper.ViewContext.RouteData.Values["action"].ToString();

            breadcrumb.Append("<li class='breadcrumb-item'>");
            breadcrumb.Append(helper.ActionLink(controllerName, "Index", controllerName).ToHtmlString());
            breadcrumb.Append("</li>");

            if (actionName != "Index")
            {
                breadcrumb.Append("<li class='breadcrumb-item active text-info font-weight-medium' aria-current='page'>");
                breadcrumb.Append(helper.ActionLink(actionName, actionName, controllerName).ToHtmlString());
                breadcrumb.Append("</li>");
            }

            breadcrumb.Append("</ol>");

            return new MvcHtmlString(breadcrumb.ToString());
        }
    }
}

