using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NLORM.Core
{
    public interface ISqlGenerator
    {
        string GenCreateTableSqlByDbType(DbType type, string length);
    }
}
