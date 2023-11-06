using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.ViewModels
{
    public class SalaryBreakupPolicyViewModel
    {
        public SalaryBreakupElementPolicy Master { get; set; }
        public List<SalaryBreakupElementPolicyDetails> Details { get; set; }
        public List<BreakupElementPolicyDetails> PolicyDetails { get; set; }

        public string SalaryGroupName { get; set; }
        public string SalaryGroupCode { get; set; }
        public bool IsActive { get; set; }
    }

    public class SalaryElement
    {
        public string Element { get; set; }
        public string ElementCode { get; set; }
        public string Amount { get; set; }
        public string ElementName { get; set; }
    }


    public class BreakupElementPolicyDetails
    {
        public string CompanyCode { get; set; }
        public string SisterConcernCode { get; set; }
        public string SalaryGroupCode { get; set; }
        public string BreakupElementName { get; set; }
        public string BreakupElementCode { get; set; }
        public decimal Amount { get; set; }
    }
}