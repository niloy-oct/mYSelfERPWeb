using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.Services
{
    public class SalaryGroupService : ISalaryGroupService
    {
        private  readonly IBaseRepository<SalaryGroup> _salaryGroupRepository;
        private  readonly IUnitOfWork _unitOfWork;
        public SalaryGroupService(IBaseRepository<SalaryGroup> salaryGroupRepository, IUnitOfWork unitOfWork)
        {
            _salaryGroupRepository = salaryGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddSalaryGroup(SalaryGroup salaryGroup)
        {
            _salaryGroupRepository.Add(salaryGroup);
        }

        public void UpdateSalaryGroup(SalaryGroup salaryGroup)
        {
            _salaryGroupRepository.Update(salaryGroup);
        }

        public void SaveSalaryGroup()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<SalaryGroup> GetAllSalaryGroups()
        {
            if (Sessions.Name.Role != "System Admin")
            {
                return _salaryGroupRepository.GetAllAsEnumerable().Where(i => i.CompanyCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode);
            }

            return _salaryGroupRepository.GetAllAsEnumerable();
        }

        public SalaryGroup GetSalaryGroupById(int id)
        {
           return _salaryGroupRepository.FindBy(i => i.Id == id).FirstOrDefault();
        }
    }
}