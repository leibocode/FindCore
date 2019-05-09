using FindCore.UserAPI.Data;
using FindCore.UserAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FindCore.UserAPI.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly UserContext _dbContext;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserContext userContext) : base(logger)
        {
            _dbContext = userContext;
            _logger = logger;
        }


        /// <summary>
        /// 登录用户获取个人信息
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _dbContext.Users.AsNoTracking()
                               .Include(u => u.Properties).SingleOrDefaultAsync(b => b.Id == UserIdentity.UserId);
            if (User == null)
                throw new OperationCanceledException();
            return Ok(users);
        }



        /// <summary>
        /// 检查或者创建用户
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("check-or-create")]
        public async Task<IActionResult> CheckOrCreateUser([FromBody] string phone)
        {
            //todo:phone验证
            var user = await _dbContext.Users.SingleOrDefaultAsync(b => b.Phone == phone);

            if (user == null)
            {
                user = new AppUser() { Phone = phone };
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(new
            {
                UserId = user.Id,
                user.Name,
                user.Title,
                user.Company,
                user.Avatar
            });
        }



        /// <summary>
        /// 获取用户标签数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tags")]
        public async Task<IActionResult> GetUsersTagsAsync()
        {
            return Ok(await _dbContext.UserTags.Where(b => b.AppUserId == UserIdentity.UserId).ToListAsync());
        }

        /// <summary>
        /// 通过手机号查询信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search/{phone}")]
        public async Task<IActionResult> Search(string phone)
        {
            return Ok(await _dbContext.Users.Include(b => b.Properties).SingleOrDefaultAsync(b => b.Phone == phone));
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-useringo/{id}")]
        public async Task<IActionResult> GetUserBaseInfAsync(int id)
        {
            var entity = await _dbContext.Users.SingleOrDefaultAsync(b => b.Id == id);

            if (entity == null)
            {
                _logger.LogInformation($"查询");

            }

            return Ok(new
            {
                UserId = entity.Id,
                entity.Name,
                entity.Title,
                entity.Company,
                entity.Avatar
            });
        }

        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FormBody]JsonPatchDocument<AppUser> patch)
        {
            var 
        }

        ///// <summary>
        ///// 更改标签
        ///// </summary>
        ///// <param name="tags"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("update-tags")]
        //public async Task<IActionResult> UpdateTags([FormBody]string[] tags)
        //{
        //    tags = tags ?? new string[] { };
        //    var originList = await _dbContext.UserTags
        //                          .Where(x => x.AppUserId == UserIdentity.UserId)
        //                          .Select(x => x.Tag).ToListAsync();
        //    var removeTags = originList.Except(tags).ToList();
        //    var addTags = tags.Except(originList).ToList();
        //    await _dbContext.AddRangeAsync(addTags.Select(x => new UserTag()
        //    {
        //        AppUserId = UserIdentity.UserId,
        //        Tag = x,
        //        CreateTime = DateTime.Now
        //    }));
        //    var removeList = await _dbContext.Users.Where(x=>)

        //}

    }
}
