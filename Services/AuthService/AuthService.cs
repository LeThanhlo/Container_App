using Container_App.Model;
using Container_App.Repository.AuthRepository;
using Container_App.Repository.UserRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Container_App.Services.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<Token> Login(string username, string password)
        {
            var user = await _authRepository.GetUserByUsernameAndPassword(username, password);
            if (user == null) return null;

            var token = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken(user);

            await _authRepository.SaveRefreshToken(refreshToken);

            return new Token
            {
                AccessToken = token,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<Token> RefreshToken(string refreshToken)
        {
            var storedToken = await _authRepository.GetRefreshToken(refreshToken);

            if (storedToken == null || storedToken.ExpiryDate < DateTime.UtcNow)
            {
                return null; // Invalid or expired refresh token
            }

            var user = await _authRepository.GetUserByID(storedToken.UserId);
            if (user == null) return null;

            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken(user);

            await _authRepository.RevokeRefreshToken(refreshToken); // Revoke old refresh token
            await _authRepository.SaveRefreshToken(newRefreshToken); // Save new refresh token

            return new Token
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.FullName.ToString())
        };

            var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            if (string.IsNullOrEmpty(jwtSecretKey))
            {
                throw new InvalidOperationException("JWT_SECRET_KEY must be set in the .env file.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(1), // Access token expires in 1 day
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(), // Random token string
                ExpiryDate = DateTime.UtcNow.AddDays(7), // Refresh token expires in 7 days
                UserId = user.UserId,
                IsRevoked = false
            };

            return refreshToken;
        }
    }
}
