using NLORM.Core.BasicDefinitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace NLORM.Core.Utility
{
    public class ExpandoPorpertyGetter : IPropertyGetter
    {
        public IDictionary<string, object> GetPropertyDic(FilterObject Fo)
        {
            var eoDic = Fo.Cons as IDictionary<string,object>;
            Debug.Assert(eoDic != null);
            return eoDic;            
        }
    }
}
