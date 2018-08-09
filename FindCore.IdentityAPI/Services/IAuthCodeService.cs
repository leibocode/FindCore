using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.IdentityAPI.Services
{
    public interface IAuthCodeService
    {
        /// <summary>
        /// 根据手机号验证验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        Task<bool> Validate(string phone, string authCode); 
    }
}
