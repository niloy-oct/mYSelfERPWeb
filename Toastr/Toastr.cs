using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb
{
    [Serializable]
    public class Toastr
    {
        public bool ShowNewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public List<ToastMessage> ToastMessages { get; set; }

        public Toastr()
        {
            ToastMessages = new List<ToastMessage>();
            ShowNewestOnTop = false;
            ShowCloseButton = false;
        }

        public ToastMessage AddToastMessage(string title, string message, ToastType toastType)
        {
            var toast = new ToastMessage()
            {
                Title = title,
                Message = message,
                ToastType = toastType
            };
            ToastMessages.Add(toast);
            return toast;
        }
    }
}