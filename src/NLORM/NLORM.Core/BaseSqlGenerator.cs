using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using NLORM.Core.BasicDefinitions;

namespace NLORM.Core
{
    public class BaseSqlGenerator : ISqlGenerator
    {
        private const string StringDeafultLength = "255";
        public string GenCreateTableSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append("CREATE TABLE " + md.TableName + " (");
            var i = 1;
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
                    ret = GenCreateInteger(cfd);
                    break;
                case DbType.Int32:
                    ret = GenCreateInteger(cfd);
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
                    ret = GenCreateDateTime(cfd);
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
            var ret = "";
            ret += " " + cfd.ColumnName + " ";
            var length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            var nullable = cfd.Nullable ? StringDeafultLength : "not null";
            ret += "varchar(" + length + ") " + nullable;
            return ret;
        }

        virtual public string GenCreateInteger(ColumnFieldDefinition cfd)
        {
            var ret = "";
            ret += " " + cfd.ColumnName + " ";
            var nullable = cfd.Nullable ? StringDeafultLength : "not null";
            ret += "INTEGER " + nullable;
            return ret;
        }


        virtual public string GenCreateDateTime(ColumnFieldDefinition cfd)
        {
            var ret = "";
            ret += " " + cfd.ColumnName + " ";
            var nullable = cfd.Nullable ? StringDeafultLength : "not null";
            ret += "DATETIME " + nullable;
            return ret;
        }


        virtual public string GenDropTableSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" DROP TABLE ");
            ret.Append(md.TableName);
            return ret.ToString();
        }

        virtual public string GenInsertSql(ModelDefinition md)
        {
            var ret = new StringBuilder();

            var fields = new StringBuilder();
            var valueFields = new StringBuilder();
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

        virtual public string GenSelectSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" SELECT * FROM ");
            ret.Append(md.TableName + " ");
            return ret.ToString();
        }

        private string GenInsertColFields(ModelDefinition md)
        {
            var ret = "";
            var i = 1;
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
            var ret = "";
            var i = 1;
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
