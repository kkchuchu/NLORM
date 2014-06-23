using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLORM.Core.BasicDefinitions;

namespace NLORM.Core
{
    public class BaseSqlBuilder : ISqlBuilder
    {
        protected ISqlGenerator SqlGen;

        public BaseSqlBuilder()
        {
            SqlGen = new BaseSqlGenerator();
        }
        public string GenCreateTableSql<T>() where T : new()
        {
            var mdc = new ModelDefinitionConverter(null);
            ModelDefinition md = mdc.ConverClassToModelDefinition<T>();
            StringBuilder ret = new StringBuilder();
            ret.Append("CREATE TABLE " + md.TableName + " (");
            int i = 1;
            foreach (var cfd in md.PropertyColumnDic.Values)
            {
                ret.Append(SqlGen.GenCreateTableSql(cfd));
                if (i != md.PropertyColumnDic.Values.Count)
                {
                    ret.Append(" ,");
                }
                i++;
            }
            ret.Append(")");
            return ret.ToString();
        }


    }
}
