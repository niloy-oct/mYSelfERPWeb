using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mYSelfERPWeb.Models
{
    [Table("Administration_User_menu")]
    public class UserMenu
    {
        [Key]
        public int id { get; set; }
        public int RoleId { get; set; }
        public int menu_id { get; set; }
        public int sub_menu_id { get; set; }
        public int nested_menu_id { get; set; }
        public int Module_Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
    }
}