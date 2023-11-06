using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mYSelfERPWeb.Extensions;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;

namespace mYSelfERPWeb.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<Company> _companyRepository;
        private readonly IBaseRepository<SisterConcern> _sisterConcernRepository;

        public UserService(IBaseRepository<User> userRepository, IUnitOfWork unitOfWork, IBaseRepository<Role> roleRepository, IBaseRepository<Company> companyRepository, IBaseRepository<SisterConcern> sisterConcernRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _companyRepository = companyRepository;
            _sisterConcernRepository = sisterConcernRepository;
        }

        public User GetUserByUserIdPassword(string userId, string password)
        {
            return _userRepository.GetAllData().FirstOrDefault(i => i.UserID == userId);
        }


        public IEnumerable<UserViewModel> GetUsers(string roleName)
        {
            return _userRepository.GetAllUsers(_roleRepository, roleName);
        }


        public IEnumerable<LogInViewModel> GetLoginDetailsByUserID(string userId)
        {
            return _userRepository.GetLoginDetailsByUserID(_roleRepository, _companyRepository,
                _sisterConcernRepository, userId);
        }


        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void SaveUser()
        {
            _unitOfWork.Commit();
        }

        public User GetUserById(int id)
        {
            return _userRepository.FindBy(i => i.Id == id).FirstOrDefault();
        }

        public User GetUserByUserId(string userid)
        {
          return  _userRepository.FindBy(i => i.UserID == userid).FirstOrDefault();
        }

        public Tuple<string, string> HashPassword(string password, int workFactor = 12)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(workFactor);
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return new Tuple<string, string>(hash, salt);
        }


        public bool VerifyPassword(string password, string salt, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

    }
}