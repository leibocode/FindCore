using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.ContactAPI.Models.VModels
{
    public class contactTagVModel
    {
        public contactTagVModel()
        {
            Tags = new List<string>();
        }

        public int UserId { get; set; }

        /// <summary>
        /// 标签信息
        /// </summary>
        public List<string> Tags { get; set; } 
    }
}
