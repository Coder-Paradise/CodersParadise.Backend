using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Repositories;
using CodersParadise.DataAccess.Databases.CodersParadise;
using CodersParadise.DataAccess.Databases.CodersParadise.Models;

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
                Id = user.Id,
                Email = user.Email,
                VerifiedDate = user.VerifiedDate,
                PasswordSalt = user.PasswordSalt,   
                PasswordHash = user.PasswordHash
            };
        }

        public async Task<Core.Models.User?> GetUserByToken(string token)
        {
            var user = _dbContext.Users.Where(x => x.VerificationToken == token).FirstOrDefault();

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

        public async Task<bool> UpdateUserVerifiedDate(DateTime verifiedDate)
        {
            var user = new User() { Id = 2, VerifiedDate = verifiedDate };
            //using (var db = new MyEfContextName())
            //{
            _dbContext.Users.Attach(user);
            _dbContext.Entry(user).Property(x => x.VerifiedDate).IsModified = true;
            _dbContext.SaveChanges();
            //}

            return true;

            //var user = _dbContext.Users.Where(x => x.VerificationToken == token).FirstOrDefault();

            //if (user == null) return null;

            //return new Core.Models.User
            //{
            //    Id = user.Id,
            //    Email = user.Email,
            //    VerifiedDate = user.VerifiedDate,
            //    PasswordSalt = user.PasswordSalt,
            //    PasswordHash = user.PasswordHash
            //};
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
