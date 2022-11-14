using CodersParadise.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodersParadise.Core.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<Models.User?> GetUserByEmail(string email);
    }
}
