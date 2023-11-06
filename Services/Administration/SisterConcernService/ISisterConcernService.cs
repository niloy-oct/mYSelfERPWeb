using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public interface ISisterConcernService
    {
        void AddSisterConcern(SisterConcern sisterConcern);
        void UpdateSisterConcern(SisterConcern sisterConcern);
        void SaveSisterConcern();
        SisterConcern GetSisterConcernById(int id);
        SisterConcern GetSisterConcernByCode(string code);
        List<SisterConcern> GetSisterConcernByCompanyCode(string companycode);
        IEnumerable<SisterConcern> GetAllSisterConcerns();

        IEnumerable<SisterConcernViewModel> GetAllSisterConcernsWithCompanyName(string roleName);
    }
}
