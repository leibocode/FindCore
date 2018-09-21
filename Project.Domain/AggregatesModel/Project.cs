using Project.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.AggregatesModel
{
    /// <summary>
    /// 项目实体
    /// </summary>
    public class Project:IAggregateRoot
    {
        public int Id { get; set; }

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


        /// <summary>
        /// 转换后的BP文件地址
        /// </summary>
        public string FormatBPFile { get; set; }

        /// <summary>
        /// 是否显示敏感信息
        /// </summary>
        public bool ShowSecurityInfo { get; set; }

        public int ProvinceId { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int AreaId { get; set; }

        public string AreaName { get; set; }

        /// <summary>
        /// 公司成立时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 公司基本信息
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 融资阶段
        /// </summary>
        public string FinStage { get; set; }

        /// <summary>
        /// 融资金额 单位(万)
        /// </summary>
        public string FinPercentage { get; set; }

        /// <summary>
        /// 收入(万元)
        /// </summary>
        public int Income { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Revenue { get; set; }

        /// <summary>
        /// 估值(万元)
        /// </summary>
        public int Valuation { get; set; }

        /// <summary>
        ///佣金分配方式
        /// </summary>
        public int BrokeageOptions { get; set; }

        /// <summary>
        /// 是否委托给平台
        /// </summary>
        public bool OnPlatform { get; set; }

       
    }
}
