using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.Services
{
    public class CompanyService : ICompanyService
    {

        private static IBaseRepository<Company> _companyRepository;
        private static IUnitOfWork _unitOfWork;

        public CompanyService(IBaseRepository<Company> companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddCompany(Company company)
        {
            _companyRepository.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            _companyRepository.Update(company);
        }

        public void SaveCompany()
        {
            _unitOfWork.Commit();
        }

        public Company GetCompanyById(int id)
        {
            return _companyRepository.FindBy(i => i.Id == id).FirstOrDefault();
        }

        public Company GetCompanyByCode(string code)
        {
            return _companyRepository.FindBy(i => i.ComCode == code).FirstOrDefault();
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            if (Sessions.Name.Role != "System Admin")
            {
                return _companyRepository.GetAllAsEnumerable().Where(i => i.ComCode == Sessions.Name.CompanyCode);
            }

            return _companyRepository.GetAllAsEnumerable();
        }



    }
}