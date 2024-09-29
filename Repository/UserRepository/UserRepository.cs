using Container_App.Model;
using Container_App.utilities;

namespace Container_App.Repository.UserRepository
{
    public class UserRepository: IUserRepository
    {
        private readonly List<User> _users = new();
        public UserRepository()
        {
            // Dữ liệu giả lập
            _users.Add(new User { UserId = 1, Username = "longthanhltl", Password = "longthanhltl", FullName = "Le Thanh Long", RoleGroupId = 1 });
            _users.Add(new User { UserId = 2, Username = "tienngo", Password = "tienngo", FullName = "Ngo Thanh Tien", RoleGroupId = 2 });
            _users.Add(new User { UserId = 2, Username = "levi", Password = "levi", FullName = "Le Vi", RoleGroupId = 2 });
            _users.Add(new User { UserId = 2, Username = "khavutran", Password = "khavutran", FullName = "Trần Vũ Kha", RoleGroupId = 2 });
            _users.Add(new User { UserId = 2, Username = "trangthuy", Password = "trangthuy", FullName = "Pham Thuy Trang", RoleGroupId = 2 });
        }


        public Task<List<User>> GetUsers(PagedResult page)
        {
            // Bắt đầu với danh sách tất cả người dùng
            var query = _users.AsQueryable();

            // Nếu có từ khóa tìm kiếm, lọc kết quả theo từ khóa
            if (!string.IsNullOrEmpty(page.SearchTerm))
            {
                query = query.Where(u => u.FullName.Contains(page.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Phân trang: bỏ qua (pageNumber - 1) * pageSize số lượng phần tử đầu tiên và lấy tiếp pageSize phần tử
            var pagedUsers = query
                .Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize)
                .ToList();

            // Trả về kết quả dưới dạng Task
            return Task.FromResult(pagedUsers);
        }


        public Task<User> GetUserById(int id) => Task.FromResult(_users.FirstOrDefault(u => u.UserId == id));

        public Task<User> CreateUser(User user)
        {
            user.UserId = _users.Max(u => u.UserId) + 1;
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User> UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.FullName = user.FullName;
                existingUser.RoleGroupId = user.RoleGroupId;
            }
            return Task.FromResult(existingUser);
        }

        public Task DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _users.Remove(user);
            }
            return Task.CompletedTask;
        }

        public Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return Task.FromResult(user);
        }

    }
}
