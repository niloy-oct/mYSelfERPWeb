using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.Services
{
    public interface ISalaryGroupService
    {
        void AddSalaryGroup(SalaryGroup salaryGroup);
        void UpdateSalaryGroup(SalaryGroup salaryGroup);
        void SaveSalaryGroup();
        IEnumerable<SalaryGroup> GetAllSalaryGroups();

        SalaryGroup GetSalaryGroupById(int id);
    }
}
