using Container_App.Model;
using Container_App.Repository.UserRepository;
using Container_App.utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Container_App.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<List<User>> GetUsers(PagedResult page) => _userRepository.GetUsers(page);

        public Task<User> GetUserById(int id) => _userRepository.GetUserById(id);

        public Task<User> CreateUser(User user) => _userRepository.CreateUser(user);

        public Task<User> UpdateUser(int id, User user)
        {
            user.UserId = id; // Đảm bảo UserId được thiết lập
            return _userRepository.UpdateUser(user);
        }

        public Task DeleteUser(int id) => _userRepository.DeleteUser(id);

    }
}
