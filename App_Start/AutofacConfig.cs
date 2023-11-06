using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using mYSelfERPWeb.Services;

namespace mYSelfERPWeb.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DatabaseContext.DatabaseContext>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterInstance(new AutoMapperConfiguration().Configure()).As<IMapper>().SingleInstance();
            builder.Register(c => new HttpClient()).As<HttpClient>().InstancePerRequest();

            #region Repository

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerRequest();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerRequest();

            #endregion

            #region Services 
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerRequest();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<ModuleService>().As<IModuleService>().InstancePerRequest();
            builder.RegisterType<SalaryGroupService>().As<ISalaryGroupService>().InstancePerRequest();
            builder.RegisterType<SalaryBreakupElementService>().As<ISalaryBreakupElementService>().InstancePerRequest();
            builder.RegisterType<SalaryDeductionElementService>().As<ISalaryDeductionElementService>().InstancePerRequest();
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerRequest();
            builder.RegisterType<SisterConcernService>().As<ISisterConcernService>().InstancePerRequest();
            builder.RegisterType<SMSService>().As<ISMSService>().InstancePerRequest();
            builder.RegisterType<SalaryBreakupElementPolicyService>().As<ISalaryBreakupElementPolicyService>().InstancePerRequest();








            // miscellaneousService
            builder.RegisterGeneric(typeof(MiscellaneousService<>)).As(typeof(IMiscellaneousService<>)).InstancePerRequest();

            #endregion


            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            Autofac.IContainer container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}