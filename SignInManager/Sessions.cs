using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb
{
    public static class Sessions
    {
        public static SessionInfo Name
        {
            get
            {
                return HttpContext.Current.Session["Name"] as SessionInfo;
            }
            set
            {
                HttpContext.Current.Session["Name"] = value;
            }
        }
    }
}