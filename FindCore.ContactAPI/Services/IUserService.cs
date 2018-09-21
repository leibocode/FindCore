using FindCore.ContactAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.ContactAPI.Services
{
    public interface IUserService
    {
        Task<BaseUserInfo> GetBaseUserInfoAsync(int userId);
    }
}
