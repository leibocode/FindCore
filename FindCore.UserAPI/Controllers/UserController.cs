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


        public UserController(ILogger<BaseController> logger, UserContext userContext) : base(logger)
        {
            _dbContext = userContext;
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
        /// 用户更改个人信息
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        //[Route("")]
        //[HttpPatch]
        //public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<AppUser> patch)
        //{
        //    var entity = await _dbContext.Users.SingleOrDefaultAsync(b => b.Id == UserIdentity.UserId);

        //    //将需要更新的数据复制对象 
        //    patch.ApplyTo(entity);

        //    //如果有修改的Properties 不追踪AppUser实体的
        //    if (entity.Properties != null)
        //    {
        //        foreach (var item in entity.Properties)
        //        {
        //            _dbContext.Entry(item).State = EntityState.Detached;
        //        }

        //        //获取原来用户所有的Properties ，必须使用
        //        var originProperties = await _dbContext.UserProperties.AsNoTracking().Where(b => b.AppUserId == UserIdentity.UserId).ToListAsync();

        //        foreach (var item in originProperties)
        //        {
        //            if (!entity.Properties.Exists(b => b.Key == item.Key && b.Value == item.Value))
        //            {
        //                //如果不存在做删除操作
        //                _dbContext.Remove(item);
        //            }
        //        }

        //        foreach (var item in entity.Properties)
        //        {
        //            if (!originProperties.Exists(b => b.Key == item.Key && b.Value == item.Value))
        //            {
        //                //如果不存在新增操作
        //                _dbContext.Add(item);
        //            }
        //        }

        //        using (var transAction = _dbContext.Database.BeginTransaction())
        //        { //当用户信息进行修改时，发送更新个人属性的消息
        //            //RaiseUserProfileChangeEvent(entity);

        //        }
        //        return null;
        //    }
        //}


        /// <summary>
        /// 检查或者创建用户
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<IActionResult> CheckOrCreateUser([FromBody] string phone)
        {
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
        /// 更新用户标签数据
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("tags")]
        //public async Task<IActionResult> UpdateTags([FormBody] List<string> tags)
        //{
        //    var originTags = await _dbContext.UserTags.Where(b => b.AppUserId == UserIdentity.UserId).ToListAsync();
        //    //var newTags = tags.Except(originTags.Select(b => b.Tag));
        //    //await _dbContext.UserTags.AddRangeAsync(newTags.Select(b=>))

        //}

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
        [HttpPost]
        [Route("get-useringo/{id}")]
        public async Task<IActionResult> GetUserBaseInfAsync(int id)
        {
            var entity = await _dbContext.Users.SingleOrDefaultAsync(b => b.Id == id);

            if (entity ==null)
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
    }
}
