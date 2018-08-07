using FindCore.UserAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.UserAPI.Controllers
{
    public class BaseController: ControllerBase
    {
        public readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        public UserIdentity UserIdentity
        {
            get
            {
                var userIdentity = new UserIdentity();
                userIdentity.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(b => b.Type == "sub").Value);
                userIdentity.Title = User.Claims.FirstOrDefault(b => b.Type == "title").Value;
                userIdentity.Company = User.Claims.FirstOrDefault(b => b.Type == "company").Value;
                userIdentity.Avatar = User.Claims.FirstOrDefault(b => b.Type == "avatar").Value;
                userIdentity.Name = User.Claims.FirstOrDefault(b => b.Type == "name").Value;
                return userIdentity;
            }
        }
    }
}
