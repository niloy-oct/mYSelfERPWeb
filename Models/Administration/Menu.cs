using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mYSelfERPWeb.Models
{
    [Table("Administration_Menu")]
    public class Menu
    {
        [Key]
        public int Menu_id { get; set; }
        public string Menu_name { get; set; }
        public string Icon { get; set; }
        public int Module_Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }
    }
}