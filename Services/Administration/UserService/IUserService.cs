using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public interface IUserService
    {
        User GetUserByUserIdPassword(string userId, string password);
        IEnumerable<UserViewModel> GetUsers(string roleName);

        void AddUser(User user);
        void UpdateUser(User user);
        void SaveUser();
        User GetUserById(int id);
        User GetUserByUserId(string userid);
        IEnumerable<LogInViewModel> GetLoginDetailsByUserID(string userId);
        Tuple<string, string> HashPassword(string password, int workFactor = 12);

        bool VerifyPassword(string password, string salt, string storedHash);

    }
}
