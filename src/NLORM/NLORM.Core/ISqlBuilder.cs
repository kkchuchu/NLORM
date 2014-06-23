using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
    public interface ISqlBuilder
    {
        string GenCreateTableSql<T>() where T : new();
        string GenDropTableSql<T>() where T : new();
    }
}
