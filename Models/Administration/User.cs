using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.Models
{
    [Table("Administration_Users")]
    public class User
    {

        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }
        public string PasswordHash { get; set; }
        public string SaltKey { get; set; }
        public int RoleID { get; set; }
        public string CompanyCode { get; set; }
        public string SisterConcernCode { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }
       

        
    }
}