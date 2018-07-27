using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.AggregatesModel
{
    /// <summary>
    /// 项目实体
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 项目LOGO
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 原BP文件地址
        /// </summary>
        public string OriginBPFile { get; set; }



    }
}
