using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mYSelfERPWeb.Models
{
    [Table("Administration_Roles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}