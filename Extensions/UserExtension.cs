using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;


namespace mYSelfERPWeb.Extensions
{
    public static class UserExtension
    {
        public static IEnumerable<UserViewModel> GetAllUsers(
            this IBaseRepository<User> userRepository, IBaseRepository<Role> roleRepository, string roleName)
        {
            IQueryable<UserViewModel> result;

            if (roleName != "System Admin")
            {
                result = from user in userRepository.All.Where(i => i.CompanyCode == Sessions.Name.CompanyCode && i.SisterConcernCode == Sessions.Name.SisterConcernCode)
                         join role in roleRepository.All on user.RoleID equals role.Id
                         select new UserViewModel
                         {
                             UserID = user.UserID,
                             Id = user.Id,
                             RoleID = role.Id,
                             RoleName = role.RoleName,
                             CreationDate = user.CreationDate,
                             CreatedBy = user.CreatedBy,
                             Active = user.Active
                         };
            }
            else
            {
                result = from user in userRepository.All
                         join role in roleRepository.All on user.RoleID equals role.Id
                         select new UserViewModel
                         {
                             UserID = user.UserID,
                             Id = user.Id,
                             RoleID = role.Id,
                             RoleName = role.RoleName,
                             CreationDate = user.CreationDate,
                             CreatedBy = user.CreatedBy,
                             Active = user.Active
                         };
            }

            return result.ToList();
        }


        public static IEnumerable<LogInViewModel> GetLoginDetailsByUserID(this IBaseRepository<User> userRepository,
            IBaseRepository<Role> roleRepository, IBaseRepository<Company> companyRepository,
            IBaseRepository<SisterConcern> sisterConcernRepository, string userId)
        {


            var result = from user in userRepository.All.Where(i => i.UserID == userId)
                         join role in roleRepository.All on user.RoleID equals role.Id
                         join company in companyRepository.All on user.CompanyCode equals company.ComCode
                         join sisterConcern in sisterConcernRepository.All on user.SisterConcernCode equals sisterConcern
                             .SisterConcernCode
                         select new LogInViewModel
                         {
                             UserID = user.UserID,
                             RoleID = user.RoleID,
                             RoleName = role.RoleName,
                             CompanyCode = company.ComCode,
                             CompanyAddress = company.Address,
                             CompanyName = company.ComName,
                             CompanyLogo = company.ComLogo,
                             SisterConcernCode = sisterConcern.SisterConcernCode,
                             SisterConcernName = sisterConcern.SisterConcernName,
                             SisterConcernAddress = sisterConcern.Address,
                             SisterConcernLogo = sisterConcern.ComLogo,
                             Active = user.Active,
                             CreatedBy = user.CreatedBy,
                             CreationDate = user.CreationDate,
                             PasswordHash = user.PasswordHash,
                             SaltKey = user.SaltKey

                         };
            return result;




        }






    }
}