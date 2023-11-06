using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public interface ISalaryBreakupElementPolicyService
    {
        void AddBreakupElementPolicy(SalaryBreakupElementPolicy salaryBreakupElementPolicy);
        void SaveBreakupElementPolicy();


        void AddBreakupElementDetailsPolicy(
            List<SalaryBreakupElementPolicyDetails> salaryBreakupElementPolicyDetailsList);
        void SaveBreakupElementDetailsPolicy();
        IEnumerable<SalaryBreakupPolicyViewModel> GetDistinctSalaryGroups();
        SalaryBreakupElementPolicy GetBreakupElementPolicyBySalaryGroup(string salarygroup);
        List<BreakupElementPolicyDetails> GetBreakupElementPolicyDetailsList(string salarygroup);

        void DeleteBreakupElementPolicy(string salarygroup);

        void DeleteBreakupElementDeatilsPolicy(string salarygroup);

    }
}
