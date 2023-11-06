using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.Services
{
    public class SalaryBreakupElementService : ISalaryBreakupElementService
    {

        private readonly IBaseRepository<SalaryBreakupElement> _salaryBreakupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SalaryBreakupElementService(IBaseRepository<SalaryBreakupElement> salaryBreakupRepository,
            IUnitOfWork unitOfWork)
        {
            _salaryBreakupRepository = salaryBreakupRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddSalaryBreakupElement(SalaryBreakupElement salaryBreakupElement)
        {
            _salaryBreakupRepository.Add(salaryBreakupElement);
        }

        public IEnumerable<SalaryBreakupElement> GetAllSalaryBreakupElements()
        {

            if (Sessions.Name.Role != "System Admin")
            {
                return _salaryBreakupRepository.GetAllAsEnumerable().Where(i=>i.CompanyCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode);
            }

            return _salaryBreakupRepository.GetAllAsEnumerable();
        }

        public SalaryBreakupElement GetBreakupElementById(int id)
        {
            return _salaryBreakupRepository.FindBy(i => i.Id == id).FirstOrDefault();
        }

        public void SaveSalaryBreakupElement()
        {
           _unitOfWork.Commit();
        }

        public void UpdateSalaryBreakupElement(SalaryBreakupElement salaryBreakupElement)
        {
            _salaryBreakupRepository.Update(salaryBreakupElement);
        }
    }
}