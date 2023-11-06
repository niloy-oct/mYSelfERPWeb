using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mYSelfERPWeb
{
    public class CoreController : Controller
    {
        private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string NumericChars = "0123456789";
        private const string SpecialChars = "@";
        private static readonly Random Random = new Random();

        #region OnException
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)

                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml"
                };

            filterContext.ExceptionHandled = true;
        }
        #endregion

        public void AddToastMessage(string title, string message, ToastType toastType = ToastType.Info)
        {
            Toastr toastr = TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();
            toastr.AddToastMessage(title, message, toastType);
            TempData["Toastr"] = toastr;
        }
        public void AddAuditTrail(dynamic obj, bool IsNew)
        {
            if (IsNew)
            {
                if (obj is IEnumerable<dynamic> objects)
                {
                    foreach (var item in objects)
                    {
                        if (item.GetType().GetProperty("CreationDate") != null && item.GetType().GetProperty("CreationDate") != null)
                        {
                            item.CreatedBy = Sessions.Name.UserId;
                            item.CreationDate = GetLocalDateTime();
                        }
                    }
                }
                else
                {
                    if (obj.GetType().GetProperty("CreatedBy") != null && obj.GetType().GetProperty("CreatedBy") != null)
                    {
                        obj.CreatedBy = Sessions.Name.UserId;
                        obj.CreationDate = GetLocalDateTime();
                    }
                }
            }
            else
            {
                if (obj is IEnumerable<dynamic> objects)
                {
                    foreach (var item in objects)
                    {
                        if (item.GetType().GetProperty("ModificationDate") != null && item.GetType().GetProperty("ModificationDate") != null)
                        {
                            item.ModifiedBy = Sessions.Name.UserId;
                            item.ModificationDate = GetLocalDateTime();
                        }
                    }
                }
                else
                {
                    if (obj.GetType().GetProperty("ModifiedBy") != null && obj.GetType().GetProperty("ModifiedBy") != null)
                    {
                        obj.ModifiedBy = Sessions.Name.UserId;
                        obj.ModificationDate = GetLocalDateTime();
                    }
                }
            }
        }


        public DateTime GetLocalDateTime()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, BdZone);
            return localDateTime;
        }

        public static string GeneratePassword(int minLength = 8, int maxLength = 10)
        {
            if (minLength < 8 || maxLength < 8 || maxLength > 10 || minLength > maxLength)
                throw new ArgumentException("Invalid password length.");

            string allChars = UppercaseChars + LowercaseChars + NumericChars + SpecialChars;

            char[] password = new char[maxLength];

            // Add at least one character from each category
            password[0] = UppercaseChars[Random.Next(UppercaseChars.Length)];
            password[1] = LowercaseChars[Random.Next(LowercaseChars.Length)];
            password[2] = NumericChars[Random.Next(NumericChars.Length)];
            password[3] = SpecialChars[Random.Next(SpecialChars.Length)];

            // Fill the remaining characters
            for (int i = 4; i < maxLength; i++)
            {
                password[i] = allChars[Random.Next(allChars.Length)];
            }

            // Shuffle the characters
            for (int i = 0; i < maxLength - 1; i++)
            {
                int j = Random.Next(i, maxLength);
                char temp = password[i];
                password[i] = password[j];
                password[j] = temp;
            }

            return new string(password);
        }

        public static string[] GetFirstAndLastDatesOfMonth()
        {
            DateTime today = DateTime.Today;
            DateTime firstDateOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDateOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

            string[] dates = {
                firstDateOfMonth.ToString("dd/MM/yyyy"),
                lastDateOfMonth.ToString("dd/MM/yyyy")
            };

            return dates;
        }
    }
}