using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NLORM.Core
{
    public class NLORMBaseDb 
    {
        protected ISqlBuilder sqlBuilder;
        protected IDbConnection dbc;

    }
}
