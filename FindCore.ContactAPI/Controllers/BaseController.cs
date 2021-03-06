﻿using FindCore.ContactAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.ContactAPI.Controllers
{
    public class BaseController:ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected UserIdentity UserIdentity
        {
            get
            {
                var userIdentity = new UserIdentity();
                userIdentity.UserId = Convert.ToInt16(User.Claims.FirstOrDefault(b => b.Type == "sub").Value);
                userIdentity.Title = User.Claims.FirstOrDefault(b => b.Type == "title").Value;
                userIdentity.Company = User.Claims.FirstOrDefault(b => b.Type == "company").Value;
                userIdentity.Avatar = User.Claims.FirstOrDefault(b => b.Type == "avatar").Value;
                userIdentity.Name = User.Claims.FirstOrDefault(b => b.Type == "name").Value;
                return userIdentity;
            }
        }
    }
}
