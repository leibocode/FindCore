using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace Project.Domain.Events
{
    /// <summary>
    /// 创建项目
    /// </summary>
    public class ProjectCreatedEvent:INotification
    {
        public AggregatesModel.Project Project { get; set; }
    }
}
