using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Repositories;
using CodersParadise.DataAccess.Databases.CodersParadise;
using CodersParadise.DataAccess.Databases.CodersParadise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var user = _dbContext.Users.Where(x => x.Email == email).FirstOrDefault();

            if (user == null) return null;

            return new Core.Models.User
            {
                Email = user.Email
            };
        }

        public async Task<bool> Register(UserRegisterRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                PaswordHash = request.PaswordHash,
                PasswordSalt = request.PasswordSalt,
                VerificationToken = request.VerificationToken,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
