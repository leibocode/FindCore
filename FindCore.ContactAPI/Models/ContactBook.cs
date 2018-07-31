using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.ContactAPI.Models
{
    /// <summary>
    /// 通讯录
    /// </summary>
    public class ContactBook
    {
        public ContactBook()
        {
            Contacts = new List<Contact>();
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        public List<Contact> Contacts { get; set; } 
    }
}
