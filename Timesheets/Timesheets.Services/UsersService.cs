using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Timesheets.Entities;
using Timesheets.Services.Interfaces;
using Timesheets.Entities.Dto;
using Timesheets.Entities.Dto.Authentication;
using Timesheets.DataBase.Repositories.Interfaces;

namespace Timesheets.Services
{
    public class UsersService : IUsersService
    {
        public const string SecretCode = "THIS IS SOME VERY SECRET STRING!!! Im blue da ba dee da ba di da ba dee da ba di da d ba dee da ba di da ba dee";
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancelToken)
        {
            var user = await _repository.GetByIdAsync(id, cancelToken);
            if(user is null)
            {
                return null;
            }
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age,
            };
        }
        public async Task<User> GetModelByIdAsync(Guid id, CancellationToken cancelToken)
        {
            return await _repository.GetByIdAsync(id, cancelToken);
        }
        public async Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancelToken)
        {
            return await _repository.GetByRefreshToken(refreshToken, cancelToken);
        }
        public async Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancelToken)
        {
            byte[] salt = GenerateSalt(16);
            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age,
                Login = request.Login,
                PasswordHash = GetPasswordHash(request.Password, salt),
                PasswordSalt = salt,
            };
            await _repository.AddAsync(user, cancelToken);
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age,
            };
        }
        public async Task<LoginResponse> AuthenticateAsync(LoginRequest request, CancellationToken cancelToken)
        {
            var user = await _repository.GetByLoginAsync(request.Login, cancelToken);
            if(user is null)
            {
                return null;
            }

            string passwordHash = GetPasswordHash(request.Password, user.PasswordSalt);

            if(string.CompareOrdinal(passwordHash, user.PasswordHash) != 0)
            {
                return null;
            }
            var token = new LoginResponse
            {
                AccessToken = GenerateJwtToken(user.Id, 10),
                RefreshToken = GenerateRefreshToken(user.Id).Token,
            };
            user.RefreshToken = token.RefreshToken;
            await _repository.UpdateByIdAsync(user, cancelToken);
            return token;
        }
        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancelToken)
        {
            var user = await _repository.GetByRefreshToken(refreshToken, cancelToken);
            if (user is null)
            {
                return null;
            }
            var token = new LoginResponse
            {
                AccessToken = GenerateJwtToken(user.Id, 10),
                RefreshToken = GenerateRefreshToken(user.Id).Token,
            };
            user.RefreshToken = token.RefreshToken;
            await _repository.UpdateByIdAsync(user, cancelToken);
            return token;
        }
        private RefreshToken GenerateRefreshToken(Guid id)
        {
            return new RefreshToken
            {
                Expires = DateTime.Now.AddMinutes(360),
                Token = GenerateJwtToken(id, 360),
            };
        }
        private string GenerateJwtToken(Guid id, int minutes)
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
        private string GetPasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                            password: password,
                                                            salt: salt,
                                                            prf: KeyDerivationPrf.HMACSHA256,
                                                            iterationCount: 100_000,
                                                            numBytesRequested: 256 / 8));
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
    }
}