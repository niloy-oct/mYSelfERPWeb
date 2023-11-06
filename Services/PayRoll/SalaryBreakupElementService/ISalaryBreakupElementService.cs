using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;


namespace mYSelfERPWeb.Services
{
    public interface ISalaryBreakupElementService
    {
        void AddSalaryBreakupElement(SalaryBreakupElement salaryBreakupElement);
        void UpdateSalaryBreakupElement(SalaryBreakupElement salaryBreakupElement);
        void SaveSalaryBreakupElement();
        IEnumerable<SalaryBreakupElement> GetAllSalaryBreakupElements();
        SalaryBreakupElement GetBreakupElementById(int id);
    }
}
