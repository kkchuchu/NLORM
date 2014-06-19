using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using NLORM.Core;
using NLORM.Core.BasicDefinitions;

namespace NLORM.SQLite
{
    public class SQLiteSqlBuilder : ISqlBuilder
    {

        private const string StringDeafultLength = "255";

        public string GenCreateTableSql<T>() where T : new()
        {
            var mdc = new ModelDefinitionConverter(null);
            ModelDefinition md = mdc.ConverClassToModelDefinition<T>();
            StringBuilder ret = new StringBuilder();
            ret.Append("CREATE TABLE " + md.TableName+" (");
            int i = 1;
            foreach (var cfd in md.PropertyColumnDic.Values)
            {
                ret.Append(ConvertColFieldToCreateSql(cfd));
                if (i != md.PropertyColumnDic.Values.Count)
                {
                    ret.Append(" ,");
                }
                i++;
            }
            ret.Append(")");
            return ret.ToString();
        }

        private string ConvertColFieldToCreateSql(ColumnFieldDefinition cfd)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append(cfd.ColumnName + " ");
            switch (cfd.FieldType)
            {
                case DbType.String:
                    ret.Append(CreateStringCreateSql(cfd));
                    break;
                case DbType.AnsiString:
                    ret.Append(CreateStringCreateSql(cfd));
                    break;

                //TODO Not Done , only string now

                default:
                    Debug.Assert(false, "Not Support Type");
                    break;
            }
            return ret.ToString();
        }

        private string CreateStringCreateSql(ColumnFieldDefinition cfd)
        {
            string ret = "";
            string length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            string nullable = cfd.Nullable ? StringDeafultLength:"not null";
            ret += "varchar(" + length + ") " + nullable;
            return ret;
        }
    }
}
