using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using mYSelfERPWeb.Models;


namespace mYSelfERPWeb.ViewModels.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region Menu,MenuViewModel
            CreateMap<Menu, MenuViewModel>()
                ;
            #endregion

            #region SubMenu,SubMenuViewModel
            CreateMap<SubMenu, SubMenuViewModel>()
                ;
            #endregion

            #region NestedMenu,NestedMenuViewModel
            CreateMap<NestedMenu, NestedMenuViewModel>()
                ;
            #endregion

            #region User,UserViewModel
            CreateMap<User, UserViewModel>()
                ;
            #endregion
            #region Module,ModuleViewModel
            CreateMap<Module, ModuleViewModel>()
                ;
            #endregion

            #region SalaryGroup,SalaryGroupViewModel
            CreateMap<SalaryGroup, SalaryGroupViewModel>()
                ;
            #endregion

            #region SalaryBreakupElement,SalaryBreakupElementViewModel
            CreateMap<SalaryBreakupElement, SalaryBreakupElementViewModel>()
                ;
            #endregion
            #region SalaryDeductionElement,SalaryDeductionElementViewModel
            CreateMap<SalaryDeductionElement, SalaryDeductionElementViewModel>()
                ;
            #endregion

            #region Company,CompanyViewModel
            CreateMap<Company, CompanyViewModel>()
                ;
            #endregion


            #region SisterConcern,SisterConcernViewModel
            CreateMap<SisterConcern, SisterConcernViewModel>()
                ;
            #endregion
        }
    }
}