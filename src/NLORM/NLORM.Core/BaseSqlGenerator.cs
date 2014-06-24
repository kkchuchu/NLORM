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
                case DbType.Byte:
                    throw new NotImplementedException();
                    break;
                case DbType.SByte:
                    throw new NotImplementedException();
                    break;
                case DbType.Int16:
                    throw new NotImplementedException();
                    break;
                case DbType.UInt16:
                    throw new NotImplementedException();
                    break;
                case DbType.Int64:
                    throw new NotImplementedException();
                    break;
                case DbType.UInt64:
                    throw new NotImplementedException();
                    break;
                case DbType.Single:
                    throw new NotImplementedException();
                    break;
                case DbType.Double:
                    throw new NotImplementedException();
                    break;
                case DbType.Decimal:
                    throw new NotImplementedException();
                    break;
                case DbType.Boolean:
                    throw new NotImplementedException();
                    break;
                case DbType.String:
                    ret = GenCreateString(cfd);
                    break;
                case DbType.StringFixedLength:
                    throw new NotImplementedException();
                    break;
                case DbType.Guid:
                    throw new NotImplementedException();
                    break;
                case DbType.DateTime:
                    throw new NotImplementedException();
                    break;
                case DbType.DateTimeOffset:
                    throw new NotImplementedException();
                    break;
                case DbType.Time:
                    throw new NotImplementedException();
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
            StringBuilder ret = new StringBuilder();
            ret.Append(" DROP TABLE ");
            ret.Append(md.TableName);
            return ret.ToString();
        }

        virtual public string GenInsertSql(ModelDefinition md)
        {
            StringBuilder ret = new StringBuilder();

            StringBuilder fields = new StringBuilder();
            StringBuilder valueFields = new StringBuilder();
            fields.Append(GenInsertColFields(md));
            valueFields.Append(GenInsertValueFields(md));
            ret.Append(" INSERT INTO ");
            ret.Append(md.TableName+"( ");
            ret.Append(fields);
            ret.Append(" ) VALUES ( ");
            ret.Append(valueFields);
            ret.Append(") ");
            return ret.ToString();
        }

        private string GenInsertColFields(ModelDefinition md)
        {
            string ret = "";
            int i = 1;
            foreach (var mdf in md.PropertyColumnDic.Values)
            {
                ret += " " + mdf.ColumnName;
                if (i < md.PropertyColumnDic.Values.Count)
                {
                    ret+=" ," ;
                }
                i++;
            }
            return ret;
        }
        
        private string GenInsertValueFields(ModelDefinition md)
        {
            string ret = "";
            int i = 1;
            foreach (var mdf in md.PropertyColumnDic.Values)
            {
                ret += " @" + mdf.PropName;
                if (i < md.PropertyColumnDic.Values.Count)
                {
                    ret+=" ,";
                }
                i++;
            }
            return ret;
        }
    }
}
