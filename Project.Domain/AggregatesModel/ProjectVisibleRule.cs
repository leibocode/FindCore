using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.AggregatesModel
{
    public class ProjectVisibleRule
    {
        public int ProductId { get; set; }

        /// <summary>
        ///该标签的人，是否可见
        /// </summary>
        public bool Visible { get; set; }

        public string Tags { get; set; }
    }
}
