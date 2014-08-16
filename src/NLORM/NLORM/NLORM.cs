using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM
{
    public static class NLORM
    {
        public static INLORMDb GetDb(string connectionString,DbType dbType)
        {
             return NLORMFactory.Instance.GetDb(connectionString, dbType);
        }
    }
}
