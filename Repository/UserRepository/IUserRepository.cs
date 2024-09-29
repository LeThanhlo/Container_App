using Container_App.Model;
using Container_App.utilities;

namespace Container_App.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers(PagedResult page);
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
