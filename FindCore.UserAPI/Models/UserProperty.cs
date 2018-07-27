using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.UserAPI.Models
{
    /// <summary>
    /// 用户属性
    /// </summary>
    public class UserProperty
    {
        /// <summary>
        /// 关联用户
        /// </summary>
        public int AppUserId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }
    }
}
