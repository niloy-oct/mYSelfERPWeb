using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using mYSelfERPWeb.Models;


namespace mYSelfERPWeb.ViewModels.Mapping
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region RoleViewModel, Role
            CreateMap<RoleViewModel, Role>()
                ;
            #endregion

            #region MenuViewModel, Menu
            CreateMap<MenuViewModel, Menu>()
                ;
            #endregion

            #region SubMenuViewModel, SubMenu
            CreateMap<SubMenuViewModel, SubMenu>()
                ;
            #endregion
            #region UserViewModel, User
            CreateMap<UserViewModel, User>()
                ;
            #endregion
            #region NestedMenuViewModel, NestedMenu
            CreateMap<NestedMenuViewModel, NestedMenu>()
                ;
            #endregion

            #region ModuleViewModel, Module
            CreateMap<ModuleViewModel, Module>()
                ;
            #endregion

            #region SalaryGroupViewModel, SalaryGroup
            CreateMap<SalaryGroupViewModel, SalaryGroup>()
                ;
            #endregion

            #region SalaryBreakupElementViewModel, SalaryBreakupElement
            CreateMap<SalaryBreakupElementViewModel, SalaryBreakupElement>()
                ;
            #endregion
            #region SalaryDeductionElementViewModel, SalaryDeductionElement
            CreateMap<SalaryDeductionElementViewModel, SalaryDeductionElement>()
                ;
            #endregion

            #region CompanyViewModel, Company
            CreateMap<CompanyViewModel, Company>()
                ;
            #endregion

            #region SisterConcernViewModel, SisterConcern
            CreateMap<SisterConcernViewModel, SisterConcern>()
                ;
            #endregion
        }
    }
}