using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.IdentityAPI.Services
{
    public class TestAuthCodeService : IAuthCodeService
    {
        public async Task<bool> Validate(string phone, string authCode)
        {
            return true;
        }
    }
}
