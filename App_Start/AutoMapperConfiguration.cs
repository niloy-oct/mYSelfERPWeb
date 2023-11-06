using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using mYSelfERPWeb.ViewModels.Mapping;

namespace mYSelfERPWeb.App_Start
{
    public class AutoMapperConfiguration
    {
        public IMapper Configure()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}