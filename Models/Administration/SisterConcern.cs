using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.Models
{
    [Table("Administration_SisterConcern")]
    public class SisterConcern
    {
        [Key]
        public int Id { get; set; }
        public string ComCode { get; set; }
        public string SisterConcernCode { get; set; }
        public string SisterConcernName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string WebAddress { get; set; }
        public byte[] ComLogo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}