using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mYSelfERPWeb.Models
{
    [Table("Payroll_Salary_Breakup_Policy_Details")]
    public class SalaryBreakupElementPolicyDetails
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
        public string SisterConcernCode { get; set; }
        public string SalaryGroupCode { get; set; }
        public string BreakupElementCode { get; set; }
        public decimal Amount { get; set; }
    }
}