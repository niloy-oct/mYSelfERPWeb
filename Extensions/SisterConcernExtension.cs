using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Extensions
{
    public static class SisterConcernExtension
    {
        public static IEnumerable<SisterConcernViewModel> GetAllSisterConcern(
            this IBaseRepository<SisterConcern> sisterConcernRepository, IBaseRepository<Company> companyRepository, string roleName)
        {
            IQueryable<SisterConcernViewModel> result;

            if (roleName == "System Admin")
            {
                result = from concern in sisterConcernRepository.All
                    join company in companyRepository.All on concern.ComCode equals company.ComCode
                    select new SisterConcernViewModel
                    {
                        Id = concern.Id,
                        CompanyName = company.ComName,
                        SisterConcernCode = concern.SisterConcernCode,
                        SisterConcernName = concern.SisterConcernName,
                        Address = concern.Address,
                        Email = concern.Email,
                        WebAddress = concern.WebAddress,
                        ComLogo = concern.ComLogo
                    };
            }
            else
            {
                result = from concern in sisterConcernRepository.All.Where(i => i.ComCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode)
                    join company in companyRepository.All.Where(i => i.ComCode == Sessions.Name.CompanyCode) on concern.ComCode equals company.ComCode
                    select new SisterConcernViewModel
                    {
                        Id = concern.Id,
                        CompanyName = company.ComName,
                        SisterConcernCode = concern.SisterConcernCode,
                        SisterConcernName = concern.SisterConcernName,
                        Address = concern.Address,
                        Email = concern.Email,
                        WebAddress = concern.WebAddress,
                        ComLogo = concern.ComLogo
                    };
            }

            return result.ToList();
        }

    }
}