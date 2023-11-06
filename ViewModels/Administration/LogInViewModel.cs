using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.ViewModels
{
    public class LogInViewModel
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string CompanyCode { get; set; }
        public string SisterConcernCode { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }

        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string SisterConcernName { get; set; }
        public string SisterConcernAddress { get; set; }
        public byte[] CompanyLogo { get; set; }
        public byte[] SisterConcernLogo { get; set; }
        public string PasswordHash { get; set; }
        public string SaltKey { get; set; }
    }
}