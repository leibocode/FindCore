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
        /// <returns>OKResult</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _dbContext.AppUsers.AsNoTracking()
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
            var user = await _dbContext.AppUsers.SingleOrDefaultAsync(b => b.Phone == phone);

            if (user == null)
            {
                user = new AppUser() { Phone = phone };
                await _dbContext.AppUsers.AddAsync(user);
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
            return Ok(await _dbContext.AppUsers.Where(b => b.Id == UserIdentity.UserId).ToListAsync());
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
            return Ok(await _dbContext.AppUsers.Include(b => b.Properties).SingleOrDefaultAsync(b => b.Phone == phone));
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
            var entity = await _dbContext.AppUsers.SingleOrDefaultAsync(b => b.Id == id);

            if (entity == null)
            {
                return BadRequest();

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
        /// {
        ///    op:'replace','remove',replace'
        ///    path
        ///    value
        /// }
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FormBody]JsonPatchDocument<AppUser> patch)
        {
            var user = await _dbContext.AppUsers
                                       .SingleOrDefaultAsync(x => x.Id == UserIdentity.UserId);
            if (user == null)
            {
                return BadRequest();
            }
            //将需要更新的数据复制给对象
            patch.ApplyTo(user);
            //不使用 EF 进行追踪appUser实体的properties属性
            foreach (var item in user?.Properties)
            {
                _dbContext.Entry(item).State = EntityState.Detached;
            }
            var currentPro = user.Properties;
            var originPros = await _dbContext.AppUserProperties
                                   .AsNoTracking().Where(b => b.AppUserId == UserIdentity.UserId).ToListAsync();
            var allPros = originPros.Union(currentPro).Distinct();
            var addRang = allPros.Except(originPros);
            var removeRang = originPros.Except(currentPro);
             _dbContext.Remove(removeRang);
                
            using (var trans = await _dbContext.Database.BeginTransactionAsync())
            {
            
                await _dbContext.AddRangeAsync(user);
                await _dbContext.SaveChangesAsync();

                trans.Commit();
            }
            return Ok(user);
        }

        /// <summary>
        /// 更改标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-tags")]
        public async Task<IActionResult> UpdateTags([FormBody]string[] tags)
        {
             // 可空运算符
             // a ?? b 当a为null时则返回b，a不为null时则返回a本身

             tags = tags ?? new string[] { };
            var originList = await _dbContext.AppUserTags
                                 .Where(x => x.AppUserId == UserIdentity.UserId).Select(x => x.Tag).ToListAsync();
            var removeTags = originList.Except(tags).ToList();
            var addTags = tags.Except(originList).ToList();
            await _dbContext.AddRangeAsync(addTags.Select(x => new UserTag()
            {
                Tag =x,
                CreateTime =DateTime.Now,
                AppUserId = UserIdentity.UserId
            }));
            var removeList = await _dbContext.AppUserTags.Where(x => x.AppUserId == UserIdentity.UserId)
                   .Where(x => removeTags.Contains(x.Tag)).ToListAsync();
            _dbContext.RemoveRange(removeList);
            await _dbContext.SaveChangesAsync();
            return NoContent();//204
        }

        //[HttpGet]
        //[Route("baseInfo/{userid}")]
        //public async Task<IActionResult> BaseInfo(int userid)
        //{
        //    var appUser = await _dbContext.AppUsers.SingleOrDefaultAsync(x => x.Id == userid);
        //    if (appUser == null)
        //    {
        //        return BadRequest();
        //    }

        //    //var baseInfo = new BaseIn

        //}

    }
}
