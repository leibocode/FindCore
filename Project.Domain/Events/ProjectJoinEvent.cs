using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace Project.Domain.Events
{
    public class ProjectJoinEvent:INotification
    {
        public string Commpany { get; set; }


    }
}
