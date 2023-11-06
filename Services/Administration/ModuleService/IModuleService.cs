using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace mYSelfERPWeb.Services
{
    public interface IModuleService
    {
        IEnumerable<Models.Module> GetAllModules();
        void AddModule(Models.Module module);
        void UpdateModule(Models.Module module);
        void SaveModule();
        Models.Module GetModuleById(int id);
    }
}
