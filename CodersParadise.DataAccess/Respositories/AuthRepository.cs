﻿using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Repositories;
using CodersParadise.DataAccess.Databases.CodersParadise;
using CodersParadise.DataAccess.Databases.CodersParadise.Models;
using Microsoft.EntityFrameworkCore;

namespace CodersParadise.DataAccess.Respositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CodersParadiseDbContext _dbContext;

        public AuthRepository(CodersParadiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Core.Models.User?> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

            if (user == null) return null;

            return new Core.Models.User
            {
                Id = user.Id,
                Email = user.Email,
                VerifiedDate = user.VerifiedDate,
                PasswordSalt = user.PasswordSalt,   
                PasswordHash = user.PasswordHash
            };
        }

        public async Task<Core.Models.User?> GetUserByToken(string token)
        {
            var user = await _dbContext.Users.Where(x => x.VerificationToken == token).FirstOrDefaultAsync();

            if (user == null) return null;

            return new Core.Models.User
            {
                Id = user.Id,
                Email = user.Email,
                VerifiedDate = user.VerifiedDate,
                PasswordSalt = user.PasswordSalt,
                PasswordHash = user.PasswordHash
            };
        }

        public async Task<Core.Models.User?> GetUserByResetToken(string token)
        {
            var user = await _dbContext.Users.Where(x => x.PasswordResetToken == token).FirstOrDefaultAsync();

            if (user == null) return null;

            return new Core.Models.User
            {
                Id = user.Id,
                Email = user.Email,
                VerifiedDate = user.VerifiedDate,
                PasswordSalt = user.PasswordSalt,
                PasswordHash = user.PasswordHash
            };
        }

        public async Task UpdateUserVerifiedDate(int userId, DateTime verifiedDate)
        {
            await _dbContext.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(b =>
                    b.SetProperty(u => u.VerifiedDate, verifiedDate)
                );
        }

        public async Task UpdateUserResetToken(int userId, string resetToken, DateTime tokenExpiry)
        {
            await _dbContext.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(b =>
                    b.SetProperty(u => u.TokenExpiry, tokenExpiry)
                     .SetProperty(u => u.PasswordResetToken, resetToken)
                );
        }

        public async Task UpdateUserPassword(Core.Models.User user)
        {
            await _dbContext.Users
                .Where(u => u.Id == user.Id)
                .ExecuteUpdateAsync(b =>
                    b.SetProperty(u => u.PasswordHash, user.PasswordHash)
                     .SetProperty(u => u.PasswordSalt, user.PasswordSalt)
                     .SetProperty(u => u.PasswordResetToken, user.PasswordResetToken)
                     .SetProperty(u => u.TokenExpiry, user.TokenExpiry)
                );
        }

        public async Task<bool> Register(UserRegisterRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                PasswordSalt = request.PasswordSalt,
                VerificationToken = request.VerificationToken,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
