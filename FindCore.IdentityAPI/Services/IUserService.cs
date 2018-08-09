using FindCore.IdentityAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.IdentityAPI.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 检查手机是否注册，如果没有注册则创建用户
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<BaseUserInfo> GetOrCreateAsync(string phone);
    }
}
