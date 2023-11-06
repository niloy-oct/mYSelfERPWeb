using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.Services
{
    public interface ISalaryDeductionElementService
    {
        void AddSalaryDeductionElement(SalaryDeductionElement salaryDeductionElement);
        IEnumerable<SalaryDeductionElement> GetAllsalaryDeductionElements();
        SalaryDeductionElement GetSalaryDeductionElementById(int id);
        void SaveSalaryDeductionElement();
        void UpdateSalaryDeductionElement(SalaryDeductionElement salaryDeductionElement);
    }
}
