using Container_App.Model;
using Container_App.utilities;

namespace Container_App.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(PagedResult page);
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(int id, User user);
        Task DeleteUser(int id);
    }
}
