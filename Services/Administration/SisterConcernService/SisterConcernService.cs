using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Extensions;

namespace mYSelfERPWeb.Services
{
    public class SisterConcernService : ISisterConcernService
    {

        private static IBaseRepository<SisterConcern> _sisterConcernRepository;
        private static IUnitOfWork _unitOfWork;
        private static IBaseRepository<Company> _companyRepository;


        public SisterConcernService(IBaseRepository<SisterConcern> sisterConcernRepository, IUnitOfWork unitOfWork, IBaseRepository<Company> companyRepository)
        {
            _sisterConcernRepository = sisterConcernRepository;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public void AddSisterConcern(SisterConcern sisterConcern)
        {
            _sisterConcernRepository.Add(sisterConcern);
        }
        public void UpdateSisterConcern(SisterConcern sisterConcern)
        {
            _sisterConcernRepository.Update(sisterConcern);
        }
        public void SaveSisterConcern()
        {
            _unitOfWork.Commit();
        }

        
        public IEnumerable<SisterConcern> GetAllSisterConcerns()
        {

            if (Sessions.Name.Role != "System Admin")
            {
                return _sisterConcernRepository.GetAllAsEnumerable().Where(i=>i.ComCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode);
            }

            return _sisterConcernRepository.GetAllAsEnumerable();
        }

        public SisterConcern GetSisterConcernByCode(string code)
        {
            return _sisterConcernRepository.FindBy(i => i.SisterConcernCode == code).FirstOrDefault();
        }

        public List<SisterConcern> GetSisterConcernByCompanyCode(string companycode)
        {
            return _sisterConcernRepository.GetList(i => i.ComCode == companycode);
        }

        public SisterConcern GetSisterConcernById(int id)
        {
            return _sisterConcernRepository.FindBy(i => i.Id == id).FirstOrDefault();
        }

        public IEnumerable<SisterConcernViewModel> GetAllSisterConcernsWithCompanyName(string roleName)
        {

            return _sisterConcernRepository.GetAllSisterConcern(_companyRepository,roleName);
        }
    }
}