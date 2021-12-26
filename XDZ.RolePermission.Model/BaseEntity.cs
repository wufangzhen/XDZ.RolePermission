using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDZ.RolePermission.Model
{
    //TODO:后续可删除的类
    public abstract class BaseEntity
    {
        public abstract short DelFlag { get; set; }
    }
}
