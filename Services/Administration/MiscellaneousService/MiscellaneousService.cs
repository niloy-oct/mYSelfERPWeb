using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace mYSelfERPWeb.Services
{
    public class MiscellaneousService<T> : IMiscellaneousService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;


        public MiscellaneousService(IBaseRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public string GetUniqueKey(Func<T, int> codeSelector)
        {
            if (_repository.All.Any())
            {
                return (_repository.All.ToList().Max(codeSelector) + 1).ToString("D2");
            }
            else
            {
                return "01";
            }
        }

        public T GetDuplicateEntry(Expression<Func<T, bool>> duplicateSelector)
        {
            return _repository.All.Where(duplicateSelector).FirstOrDefault();
        }

        public string GetConcernUniqueKey(Func<T, int> codeSelector)
        {
            if (_repository.All.Any())
            {
                return (_repository.All.ToList().Max(codeSelector) + 1).ToString("D3");
            }
            else
            {
                return "001";
            }
        }
    }
}