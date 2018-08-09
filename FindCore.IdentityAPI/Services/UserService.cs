using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FindCore.IdentityAPI.Models.Dtos;

namespace FindCore.IdentityAPI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;


        public Task<BaseUserInfo> GetOrCreateAsync(string phone)
        {
            throw new NotImplementedException();
        }
    }
}
