using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.Models
{
    [Table("Payroll_Salary_Breakup_Policy")]
    public class SalaryBreakupElementPolicy
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
        public string SisterConcernCode { get; set; }
        public string number { get; set; }
        public string salary_group_code { get; set; }
        public string grade_lavel_code { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}