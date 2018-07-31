using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FindCore.ContactAPI.Models;
using FindCore.ContactAPI.Models.Dtos;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace FindCore.ContactAPI.Data
{
    public class MongoContactBookRepository : IContactBookRepository
    {

        private readonly ContactContext _context;

        private readonly ILogger<MongoContactBookRepository> _logger;

        public MongoContactBookRepository(ContactContext contactContext,ILogger<MongoContactBookRepository> logger)
        {
            _context = contactContext;
            _logger = logger;
        }

        public async Task<bool> AddContactAsync(int userId, BaseUserInfo baseUserInfo, CancellationToken cancellationToken)
        {
            //检查用户
            if ((await _context.ContactBooks.CountDocumentsAsync(b=>b.UserId==userId,null,cancellationToken))==0)
            {
                _logger.LogInformation($"为用户{userId}创建通讯录");
                //创建通讯录
                await _context.ContactBooks.InsertOneAsync(new ContactBook { UserId = userId }, null, cancellationToken);
            }
            //filter
            var filter = Builders<ContactBook>.Filter.Eq(b => b.UserId, userId);
            //update
            var update = Builders<ContactBook>.Update.AddToSet(b => b.Contacts, new Contact {
                UserId =baseUserInfo.UserId,
                Name =baseUserInfo.Name,
                Title = baseUserInfo.Title,
                Company = baseUserInfo.Company,
                Avatar = baseUserInfo.Avatar
            });
            //查询并添加
            var result = await _context.ContactBooks.UpdateOneAsync(filter, update, null, cancellationToken);

            return result.MatchedCount == result.ModifiedCount && result.ModifiedCount == 1;
        }

        /// <summary>
        /// 获取用户通讯录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  async Task<List<Contact>> GetContactAsync(int userId, CancellationToken cancellationToken)
        {
            var contactBook =  await (await _context.ContactBooks.FindAsync(b => b.UserId == userId)).FirstOrDefaultAsync();
            if (contactBook==null)
            {
                return contactBook.Contacts;
            }
            return new List<Contact>();
        }

        /// <summary>
        /// 给用户打标签
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="contactId"></param>
        /// <param name="tags"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> TagContactAsync(int userId, int contactId, List<string> tags, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改通讯录数据
        /// </summary>
        /// <param name="baseUserInfo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInfoAsync(BaseUserInfo baseUserInfo, CancellationToken cancellationToken)
        {
            //拿到当前用户的通讯录
            var contactBook = (await _context.ContactBooks.FindAsync(b => b.UserId == baseUserInfo.UserId, null, cancellationToken)).FirstOrDefault(cancellationToken);

            if (contactBook==null)
            {
                return true;
            }

            //查找到当前用户所有好友Id
            var contactIds = contactBook.Contacts.Select(b => b.UserId);

            var filter = Builders<ContactBook>.Filter.And(
                    Builders<ContactBook>.Filter.In(x=>x.UserId,contactIds),
                    Builders<ContactBook>.Filter.ElemMatch(b=>b.Contacts,conatct=>conatct.UserId==baseUserInfo.UserId)
                );

            //设置更新
            var update = Builders<ContactBook>.Update.Set("Contacts.$.Name", baseUserInfo.Name)
                                                                   .Set("Contacts.$.Avatar", baseUserInfo.Avatar)
                                                                   .Set("Contacts.$.Title", baseUserInfo.Title)
                                                                   .Set("Contacts.$.Company", baseUserInfo.Company);

            //根据filter更新
            var updateResult = await _context.ContactBooks.UpdateManyAsync(filter, update, null, cancellationToken);

            return updateResult.MatchedCount == updateResult.ModifiedCount;

        }
    }
}
