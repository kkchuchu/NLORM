using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using NLORM.Core.BasicDefinitions;

namespace NLORM.Core
{
    public class BaseSqlGenerator : ISqlGenerator
    {
        private const string StringDeafultLength = "255";
        public string GenCreateTableSql(ModelDefinition md)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("CREATE TABLE " + md.TableName + " (");
            int i = 1;
            foreach (var cfd in md.PropertyColumnDic.Values)
            {
                ret.Append(GenColumnCreateTableSql(cfd));
                if (i != md.PropertyColumnDic.Values.Count)
                {
                    ret.Append(" ,");
                }
                i++;
            }
            ret.Append(")");
            return ret.ToString();
        }

        private string GenColumnCreateTableSql(ColumnFieldDefinition cfd)
        {
            string ret = null;
            switch (cfd.FieldType)
            {
                case DbType.String:
                    ret = GenCreateString(cfd);
                    break;
                default:
                    Debug.Assert(false,"not support type");
                    break;
            }
            return ret;
        }

        virtual public string GenCreateString(ColumnFieldDefinition cfd)
        {
            string ret = "";
            ret += " " + cfd.ColumnName + " ";
            string length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            string nullable = cfd.Nullable ? StringDeafultLength : "not null";
            ret += "varchar(" + length + ") " + nullable;
            return ret;
        }



        virtual public string GenDropTableSql(ModelDefinition md)
        {
            //TODO
            return "";
        }


    }
}
