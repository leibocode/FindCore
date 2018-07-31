using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FindCore.ContactAPI.Models;
using MongoDB.Driver;

namespace FindCore.ContactAPI.Data
{
    public class MongoContactApplyRequestRepository : IContactApplyRequestRepository
    {
        private readonly ContactContext _context;

        public MongoContactApplyRequestRepository(ContactContext contactContext)
        {
            _context = contactContext;
        }

        /// <summary>
        /// 添加好友申请
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task<bool> AddRequestAsync(ContactApplyRequest request, CancellationToken cancellationToken)
        {
            ////检查是否已经存在好友申请请求
            ////filter
            //var countFilter = Builders<ContactApplyRequest>.Filter.And(
            //         Builders<ContactApplyRequest>.Filter.Eq(b=>b.UserId,request.UserId),
            //         Builders<ContactApplyRequest>.Filter.Eq(b=>b.ApplierId,request.ApplierId)                     
            //    );

            //if ((await _context.ContactApplyRequests.CountDocumentsAsync(countFilter)==1))
            //{
            //    //条件
            //    var filter = Builders<ContactApplyRequest>.Filter.And(

            //       );
            //    //如果存在,更新请求时间

            //}

            ////进行新增操作
            //await _context.ContactApplyRequests.InsertOneAsync(request, null, cancellationToken);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过好友申请
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="applierId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ApprovalAsync(int userId, int applierId, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactApplyRequest>.Filter.And(
                  Builders<ContactApplyRequest>.Filter.Eq(b=>b.UserId,userId),
                  Builders<ContactApplyRequest>.Filter.Eq(b=>b.ApplierId,applierId)
                );
            //更新的添加 好友申请，修改状态和处理时间
            var update = Builders<ContactApplyRequest>.Update
                              .Set(b => b.ApplierId, 1)
                              .Set(b => b.HandleTime, DateTime.Now);
            var result = await _context.ContactApplyRequests.UpdateOneAsync(filter, update, null, cancellationToken);

            return result.MatchedCount == result.ModifiedCount && result.ModifiedCount == 1;

        }

        /// <summary>
        /// 获取申请列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<ContactApplyRequest>> GetRequestList(int userId, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactApplyRequest>.Filter.Eq(c => c.UserId, userId);
            var requests = await _context.ContactApplyRequests.FindAsync(filter);
            return requests.ToList();
        }
    }
}
