using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.BasicDefinitions
{
    public class FliterObject
    {
        public Type ClassType { get; set; }
        public FliterType Fliter { get; set; }
        public dynamic Cons { get; set; }
    }
}
