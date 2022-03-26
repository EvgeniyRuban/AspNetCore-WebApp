﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timesheets.Entities;
using Timesheets.DataBase.Repositories.Interfaces;

namespace Timesheets.DataBase.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancelToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancelToken);
        }
        public async Task<User> GetByLoginAsync(string login, CancellationToken cancelToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login, cancelToken);
        }
        public async Task<User> GetByRefreshToken(string refreshToken, CancellationToken cancelToken)
        {
            try
            {
                return await _context.Users
                                .FirstOrDefaultAsync(
                                    u => u.RefreshToken == refreshToken,
                                    cancelToken);
            }
            catch
            {
                return null;
            }
        }
        public async Task UpdateByIdAsync(User userToUpdate, CancellationToken cancelToken)
        {
            var user = await _context.Users
                                        .FirstOrDefaultAsync(
                                            u => u.Id == userToUpdate.Id, 
                                            cancelToken);
            if(user != null)
            {
                user = new User
                {
                    Id = userToUpdate.Id,
                    Name = userToUpdate.Name,
                    Surname = userToUpdate.Surname,
                    Age = userToUpdate.Age,
                    Login = userToUpdate.Login,
                    PasswordHash = userToUpdate.PasswordHash,
                    PasswordSalt = userToUpdate.PasswordSalt,
                    RefreshToken = userToUpdate.RefreshToken,
                };
                await _context.SaveChangesAsync(cancelToken);
            }
        }
        public async Task<User> CreateAsync(User user, CancellationToken cancelToken)
        {
            var entityEntry = await _context.Users.AddAsync(user, cancelToken);
            await _context.SaveChangesAsync(cancelToken);
            return entityEntry.Entity;
        }
    }
}
