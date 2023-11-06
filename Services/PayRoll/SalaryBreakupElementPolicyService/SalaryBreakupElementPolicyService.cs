using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Extensions;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public class SalaryBreakupElementPolicyService : ISalaryBreakupElementPolicyService
    {

        private readonly IBaseRepository<SalaryBreakupElementPolicy> _salaryBreakupElementPolicyRepository;
        private readonly IBaseRepository<SalaryBreakupElementPolicyDetails> _salaryBreakupElementPolicyDetailsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<SalaryGroup> _salarygroupRepository;
        private readonly IBaseRepository<SalaryBreakupElement> _salaryBreakupelementRepository;
        public SalaryBreakupElementPolicyService(
            IBaseRepository<SalaryBreakupElementPolicy> salaryBreakupElementPolicyRepository,
            IBaseRepository<SalaryBreakupElementPolicyDetails> salaryBreakupElementPolicyDetailsRepository,
            IUnitOfWork unitOfWork, IBaseRepository<SalaryGroup> salarygroupRepository, IBaseRepository<SalaryBreakupElement> salaryBreakupelementRepository)
        {
            _salaryBreakupElementPolicyRepository = salaryBreakupElementPolicyRepository;
            _salaryBreakupElementPolicyDetailsRepository = salaryBreakupElementPolicyDetailsRepository;
            _unitOfWork = unitOfWork;
            _salarygroupRepository = salarygroupRepository;
            _salaryBreakupelementRepository = salaryBreakupelementRepository;
        }

        public void AddBreakupElementPolicy(SalaryBreakupElementPolicy salaryBreakupElementPolicy)
        {
            _salaryBreakupElementPolicyRepository.Add(salaryBreakupElementPolicy);
        }

        public void SaveBreakupElementPolicy()
        {
            _unitOfWork.Commit();
        }

        public void AddBreakupElementDetailsPolicy(List<SalaryBreakupElementPolicyDetails> salaryBreakupElementPolicyDetailsList)
        {
            _salaryBreakupElementPolicyDetailsRepository.AddRange(salaryBreakupElementPolicyDetailsList);
        }


        public void SaveBreakupElementDetailsPolicy()
        {
            _unitOfWork.Commit();
        }

        public SalaryBreakupElementPolicy GetBreakupElementPolicyBySalaryGroup(string salarygroup)
        {
            return _salaryBreakupElementPolicyRepository.FindBy(i => i.salary_group_code == salarygroup && i.CompanyCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode)
                .FirstOrDefault();
        }

        public List<BreakupElementPolicyDetails> GetBreakupElementPolicyDetailsList(string salarygroup)
        {
            return _salaryBreakupElementPolicyDetailsRepository.GetBreakupElementPolicyDetailsWithJoin(
                _salaryBreakupelementRepository, salarygroup);
        }

        public void DeleteBreakupElementPolicy(string salarygroup)
        {
            _salaryBreakupElementPolicyRepository.Delete(i =>
                i.CompanyCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode &
                i.salary_group_code == salarygroup);
        }

        public void DeleteBreakupElementDeatilsPolicy(string salarygroup)
        {
            _salaryBreakupElementPolicyDetailsRepository.DeleteRange(i => i.CompanyCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode &
                i.SalaryGroupCode == salarygroup);
        }

        public IEnumerable<SalaryBreakupPolicyViewModel> GetDistinctSalaryGroups()
        {
            return _salaryBreakupElementPolicyRepository.GetDistinctSalaryGroups(_salarygroupRepository);
        }
    }
}