using System;
using System.Collections.Generic;
using System.Linq;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Extensions
{
    public static class SalaryBreakupPolicyExtension
    {
        public static IEnumerable<SalaryBreakupPolicyViewModel> GetDistinctSalaryGroups(
            this IBaseRepository<SalaryBreakupElementPolicy> sbeRepository,
            IBaseRepository<SalaryGroup> salaryGroupRepository)
        {
            var result = (from sbe in sbeRepository.All
                          where sbe.CompanyCode == Sessions.Name.CompanyCode
                          where sbe.SisterConcernCode == Sessions.Name.SisterConcernCode
                          select sbe.salary_group_code)
                .Distinct()
                .Join(salaryGroupRepository.All,
                    sbeSalaryGroupCode => sbeSalaryGroupCode,
                    sg => sg.Code,
                    (sbeSalaryGroupCode, sg) => new SalaryBreakupPolicyViewModel
                    {
                        SalaryGroupCode = sbeSalaryGroupCode,
                        SalaryGroupName = sg.Name,
                        IsActive = sbeRepository.All
                            .Any(sbe => sbe.salary_group_code == sbeSalaryGroupCode && sbe.IsActive)
                    })
                .ToList();

            return result;
        }

        public static List<BreakupElementPolicyDetails> GetBreakupElementPolicyDetailsWithJoin(this
            IBaseRepository<SalaryBreakupElementPolicyDetails> policyRepository,
            IBaseRepository<SalaryBreakupElement> elementRepository,
            string salarygroup)
        {
            var result = from policy in policyRepository.All
                         join element in elementRepository.All on policy.BreakupElementCode equals element.Code
                         where policy.CompanyCode == Sessions.Name.CompanyCode &&
                               policy.SisterConcernCode == Sessions.Name.SisterConcernCode &&
                               policy.SalaryGroupCode == salarygroup
                         select new BreakupElementPolicyDetails
                         {
                             BreakupElementName = element.Name,
                             BreakupElementCode = policy.BreakupElementCode,
                             Amount = policy.Amount
                         };
            return result.ToList();
        }





    }
}