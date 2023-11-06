using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.Services
{
    public class SalaryDeductionElementService : ISalaryDeductionElementService
    {
        private readonly IBaseRepository<SalaryDeductionElement> _salaryDeductionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SalaryDeductionElementService(IBaseRepository<SalaryDeductionElement> salaryDeductionRepository,
            IUnitOfWork unitOfWork)
        {
            _salaryDeductionRepository = salaryDeductionRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddSalaryDeductionElement(SalaryDeductionElement salaryDeductionElement)
        {
            _salaryDeductionRepository.Add(salaryDeductionElement);
        }

        public IEnumerable<SalaryDeductionElement> GetAllsalaryDeductionElements()
        {
            if (Sessions.Name.Role != "System Admin")
            {
                return _salaryDeductionRepository.GetAllAsEnumerable().Where(i => i.CompanyCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode);
            }
            return _salaryDeductionRepository.GetAllAsEnumerable();
        }

        public SalaryDeductionElement GetSalaryDeductionElementById(int id)
        {
            return _salaryDeductionRepository.FindBy(i => i.Id == id).FirstOrDefault();
        }

        public void SaveSalaryDeductionElement()
        {
            _unitOfWork.Commit();
        }

        public void UpdateSalaryDeductionElement(SalaryDeductionElement salaryDeductionElement)
        {
            _salaryDeductionRepository.Update(salaryDeductionElement);
        }
    }
}