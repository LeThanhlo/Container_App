using Container_App.Model;

namespace Container_App.Repository.AuthRepository
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsernameAndPassword(string username, string password);
        Task<RefreshToken> GetRefreshToken(string token);
        Task SaveRefreshToken(RefreshToken refreshToken);
        Task RevokeRefreshToken(string token);
    }
}
