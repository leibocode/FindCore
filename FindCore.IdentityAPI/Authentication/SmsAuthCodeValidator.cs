using FindCore.IdentityAPI.Services;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.IdentityAPI.Authentication
{
    /// <summary>
    /// 自定义扩展identityServer4授权模式
    /// </summary>
    public class SmsAuthCodeValidator : IExtensionGrantValidator
    {
        public string GrantType => "sms_auth_code";

        private readonly IAuthCodeService _authService;

        private readonly IUserService _userService;

        public SmsAuthCodeValidator(IAuthCodeService authCodeService,IUserService userService)
        {
            _authService = authCodeService;
            _userService = userService;
        }



        

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            //从请求中获得 手机号和验证码
            var phone =context.Request.Raw["phone"];
            var code = context.Request.Raw["auth_code"];

        }
    }
}
