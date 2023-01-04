using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Repositories;
using CodersParadise.Core.Interfaces.Services;
using CodersParadise.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodersParadise.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public JwtService(IAccessTokenGenerator accessTokenGenerator)
        {
            _accessTokenGenerator = accessTokenGenerator;
        }
        public JwtAccessToken GenerateAccessToken(long userId, string userName)
        {
            var response = _accessTokenGenerator.GenerateToken(userId, userName);
            return response;
        }
    }
}
