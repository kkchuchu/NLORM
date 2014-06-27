using NLORM.Core.BasicDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
    public interface ISqlBuilder
    {
        string SQLString { get; set; }
        string GetWhereSQLString();
        string GenCreateTableSql<T>();
        string GenDropTableSql<T>();
        string GenInsertSql<T>();

        string GenSelect(Type t);
        string GenWhereCons(FliterObject fo);
        dynamic GetWhereParas();

        ISqlBuilder CreateOne();
    }
}
