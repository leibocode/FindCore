﻿using FindCore.ContactAPI.Models;
using FindCore.ContactAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FindCore.ContactAPI.Data
{
    /// <summary>
    /// 好友通讯录仓储
    /// </summary>
    public interface IContactBookRepository
    {
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="baseUserInfo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UpdateUserInfoAsync(BaseUserInfo baseUserInfo,CancellationToken cancellationToken);


        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Contact>> GetContactAsync(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// 给好友打标签
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="contactId">好友ID</param>
        /// <param name="tags">标签列表</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> TagContactAsync(int userId, int contactId, List<string> tags, CancellationToken cancellationToken);

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="baseUserInfo">待添加的用户信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AddContactAsync(int userId, BaseUserInfo baseUserInfo, CancellationToken cancellationToken);
    }
}
