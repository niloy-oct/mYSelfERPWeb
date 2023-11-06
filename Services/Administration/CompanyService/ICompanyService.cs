using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.Services
{
    public interface ICompanyService
    {
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void SaveCompany();
        Company GetCompanyById(int id);
        Company GetCompanyByCode(string code);
        IEnumerable<Company> GetAllCompanies();


    }
}
