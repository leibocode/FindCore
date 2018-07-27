using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.SeedWork
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class Entity
    {
        int? _requestHashCode;
        int _Id;

        public virtual int Id
        {
            get {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

       

    }
}
