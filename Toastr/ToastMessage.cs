using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb
{
    [Serializable]
    public class ToastMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ToastType ToastType { get; set; }
        public bool IsSticky { get; set; }
    }
    public enum ToastType
    {
        Error,
        Info,
        Success,
        Warning
    }
}