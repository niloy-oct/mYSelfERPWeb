using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.Models
{
    [Table("Payroll_Salary_Breakeup_Element")]
    public class SalaryBreakupElement
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CompanyCode { get; set; }
        public string SisterConcernCode { get; set; }
        public string Remarks { get; set; }
        public bool? Active { get; set; }
        public byte? IsHave { get; set; }
        public string UserGroupCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}