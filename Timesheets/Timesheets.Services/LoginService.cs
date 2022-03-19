using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Timesheets.Entities.Dto;
using Timesheets.Entities.Dto.Authentication;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Services.Interfaces;
using Timesheets.Entities;

namespace Timesheets.Services
{
    public sealed class LoginService : ILoginService
    {
        public const string SecretCode = "THIS IS SOME VERY SECRET STRING!!! Im blue da ba dee da ba di da ba dee da ba di da d ba dee da ba di da ba dee";
        private const int AccessTokenExpireLength = 10;
        private const int RefreshTokenExpireLength = 360;
        private const int SaltLength = 16;
        private readonly IUsersRepository _usersRepository;

        public LoginService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<LoginResponse> AuthenticateAsync(LoginRequest request, CancellationToken cancelToken)
        {
            var user = await _usersRepository.GetByLoginAsync(request.Login, cancelToken);
            if (user is null)
            {
                return null;
            }

            string passwordHash = GetPasswordHash(request.Password, user.PasswordSalt);

            if (string.CompareOrdinal(passwordHash, user.PasswordHash) != 0)
            {
                return null;
            }
            var response = new LoginResponse
            {
                AccessToken = GenerateAccessToken(user.Id).Token,
                RefreshToken = GenerateRefreshToken(user.Id).Token,
            };
            user.RefreshToken = response.RefreshToken;
            await _usersRepository.UpdateByIdAsync(user, cancelToken);
            return response;
        }
        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancelToken)
        {
            var user = await _usersRepository.GetByRefreshToken(refreshToken, cancelToken);
            if (user is null)
            {
                return null;
            }
            var token = new LoginResponse
            {
                AccessToken = GenerateAccessToken(user.Id).Token,
                RefreshToken = GenerateRefreshToken(user.Id).Token,
            };
            user.RefreshToken = token.RefreshToken;
            await _usersRepository.UpdateByIdAsync(user, cancelToken);
            return token;
        }
        public async Task<UserResponse> RegisterAsync(CreateUserRequest request, CancellationToken cancelToken)
        {
            if(request is null)
            {
                return null;
            }

            byte[] salt = GenerateSalt(SaltLength);
            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Login = request.Login,
                Age = request.Age,
                PasswordHash = GetPasswordHash(request.Password, salt),
                PasswordSalt = salt,
            };
            var newUser = await _usersRepository.CreateAsync(user, cancelToken);

            if (newUser != null)
            {
                return new UserResponse
                {
                    Id = newUser.Id,
                    Name = newUser.Name,
                    Surname = newUser.Surname,
                    Age = newUser.Age,
                };
            }

            return null;
        }
        private RefreshToken GenerateRefreshToken(int id)
        {
            return new RefreshToken
            {
                Token = GenerateJwtToken(id, RefreshTokenExpireLength),
                Expires = DateTime.Now.AddMinutes(RefreshTokenExpireLength),
            };
        }
        private AccessToken GenerateAccessToken(int id)
        {
            return new AccessToken
            {
                Token = GenerateJwtToken(id, AccessTokenExpireLength),
                Expires = DateTime.Now.AddMinutes(AccessTokenExpireLength),
            };
        }
        private string GenerateJwtToken(int id, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }
        private string GetPasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100_000,
                numBytesRequested: 256 / 8));
        }
    }
}
