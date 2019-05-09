using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FindCore.IdentityAPI.Models.Dtos;
using Microsoft.Extensions.Logging;

namespace FindCore.IdentityAPI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;
        private readonly string _userServerUrl;

        //public UserService(IHttpClient)
        //{

        //}


        public Task<BaseUserInfo> GetOrCreateAsync(string phone)
        {
            throw new NotImplementedException();
        }
    }
}
