using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
    public class NotGenColumnAttribute : BaseAttribute
    {
        public NotGenColumnAttribute ()
        {
            this.GenCol = false;
        }
        public bool GenCol { get; set;}
    }
}
