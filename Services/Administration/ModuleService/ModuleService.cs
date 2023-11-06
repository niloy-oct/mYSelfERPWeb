using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;

namespace mYSelfERPWeb.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IBaseRepository<Module> _moduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleService(IBaseRepository<Module> moduleRepository, IUnitOfWork unitOfWork)
        {
            _moduleRepository = moduleRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Module> GetAllModules()
        {
            return _moduleRepository.GetAllAsEnumerable();
        }

        public void AddModule(Module module)
        {
            _moduleRepository.Add(module);
        }

        public void UpdateModule(Module module)
        {
            _moduleRepository.Update(module);
        }

        public void SaveModule()
        {
            _unitOfWork.Commit();
        }

        public Models.Module GetModuleById(int id)
        {
            return _moduleRepository.FindBy(i => i.Module_id == id).FirstOrDefault();
        }

    }
}