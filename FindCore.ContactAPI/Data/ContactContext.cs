 using FindCore.ContactAPI.Models;
using FindCore.ContactAPI.Models.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.ContactAPI.Data
{
    public class ContactContext
    {
        private IMongoDatabase _databse;
        private AppSetting _appSetting;

        public ContactContext(IOptionsSnapshot<AppSetting> setting)
        {
            _appSetting = setting.Value;
            var client = new MongoClient(_appSetting.MongoConnectionString);

            if (client != null)
            {
                _databse = client.GetDatabase(_appSetting.ContactDataBaseName);
            }
        }

        /// <summary>
        /// 确保表空间已经创建
        /// </summary>
        /// <param name="collectionName"></param>
        private  void CheckAndCreateCollection(string collectionName)
        {
            var collectionList = _databse.ListCollections().ToList();

            var collectionNames = new List<string>();
            //获取所有表的名称
            collectionList.ForEach(b => collectionNames.Add(b["name"].AsString));
            //判断是否创建表
            if (!collectionNames.Contains(collectionName))
            {
                _databse.CreateCollection(collectionName);
            }
        }

        //用户通讯录集合 
        public IMongoCollection<ContactBook> ContactBooks
        {
            get
            {
                CheckAndCreateCollection("ContactBook");
                return _databse.GetCollection<ContactBook>("ContactBook");
            }
        }

        //好友申请请求集合
        public IMongoCollection<ContactApplyRequest> ContactApplyRequests
        {
            get
            {
                CheckAndCreateCollection("ContactApplyRequest");
                return _databse.GetCollection<ContactApplyRequest>("ContactApplyRequest");
            }
        }
    }
}
