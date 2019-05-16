using FindCore.ContactAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindCore.ContactAPI.Services;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace FindCore.ContactAPI.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactApplyRequestRepository _contactApplyRequestRepository;

        private readonly IContactBookRepository _contactBookRepository;

        private readonly IUserService _userService;

        public ContactController(IUserService userService,
            IContactApplyRequestRepository contactApplyRequestRepository,
            ILogger<BaseController> logger,
            IContactBookRepository contactBookRepository) : base(logger)
        {
            _contactApplyRequestRepository = contactApplyRequestRepository;
            _contactBookRepository = contactBookRepository;
            _userService = userService;
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetContactsAsync(CancellationToken cancellationToken)
        {
            return Ok(await _contactBookRepository.GetContactAsync(UserIdentity.UserId, cancellationToken));
        }

        /// <summary>
        /// 获取好友申请列表
        /// </summary>
        /// <returns></returns>
        [Route("apply")]
        [HttpGet]
        public async Task<IActionResult> GetApplyRequest(CancellationToken cancellationToken)
        {
            return Ok(_contactApplyRequestRepository.GetRequestList(UserIdentity.UserId, cancellationToken));
        }
    }
}
