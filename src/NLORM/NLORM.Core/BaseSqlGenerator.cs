using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using NLORM.Core.BasicDefinitions;
using System.Dynamic;

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
                case DbType.Byte://tinyint
                    ret = GenCreateTinyint(cfd);
                    break;
                case DbType.SByte:
                    throw new NotImplementedException();
                    break;
                case DbType.Int16://smallint
                    ret = GenCreateInteger(cfd);
                    break;
                case DbType.Int32:
                    ret = GenCreateInteger(cfd);
                    break;
                case DbType.UInt16:
                    throw new NotImplementedException();
                    break;
                case DbType.Int64://bigint
                    ret = GenCreateBigint(cfd);
                    break;
                case DbType.UInt64:
                    throw new NotImplementedException();
                    break;
                case DbType.UInt32:
                    throw new NotImplementedException();
                    break;
                case DbType.Single://real
                    ret = GenCreateReal(cfd);
                    break;
                case DbType.Double://float
                    ret = GenCreateFloat(cfd);
                    break;
                case DbType.Decimal:
                    ret = GenCreateDecimal(cfd);
                    break;
                case DbType.Boolean:
                    ret = GenCreateBit(cfd);
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
                case DbType.Time://time
                    ret = GenCreateTime(cfd);
                    break;
                default:
                    Debug.Assert(false, "not support type");
                    break;
            }
            return ret;
        }

        virtual public string GenCreateString(ColumnFieldDefinition cfd)
        {
            var length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            return GenCreateSqlByType(cfd, "varchar", length);
        }

        virtual public string GenCreateInteger(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "INTEGER");
        }

        virtual public string GenCreateDateTime(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "DATETIME");
        }

        virtual public string GenCreateDecimal(ColumnFieldDefinition cfd)
        {
            var length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            return GenCreateSqlByType(cfd, "decimal", length);
        }

        virtual public string GenCreateBit(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "bit", null);
        }

        virtual public string GenCreateFloat(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "float", null);
        }

        virtual public string GenCreateReal(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "real", null);
        }

        virtual public string GenCreateTime(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "time", null);
        }

        virtual public string GenCreateTinyint(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "tinyint", null);
        }

        virtual public string GenCreateBigint(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "bigint", null);
        }

        private string GenCreateSqlByType(ColumnFieldDefinition cfd, string type, string length = "")
        {
            var ret = "";
            ret += " " + cfd.ColumnName + " ";
            var nullable = cfd.Nullable ? "" : "not null";
            if (string.IsNullOrEmpty(length))
            {
                ret += type + " " + nullable;
            }
            else
            {
                ret += type + "(" + length + ") " + nullable;
            }
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
            ret.Append(md.TableName + "( ");
            ret.Append(fields);
            ret.Append(" ) VALUES ( ");
            ret.Append(valueFields);
            ret.Append(") ");
            return ret.ToString();
        }

        virtual public string GenSelectSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" SELECT ");
            int i = 1;
            foreach (var cdf in md.PropertyColumnDic.Values)
            {
                ret.Append(cdf.ColumnName + " " + cdf.PropName);
                if (i < md.PropertyColumnDic.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
            ret.Append(" FROM " + md.TableName + " ");
            return ret.ToString();
        }

        virtual public string GenDeleteSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" DELETE ");
            ret.Append(" FROM " + md.TableName + " ");
            return ret.ToString();
        }

        public string GenUpdateSql(ModelDefinition md, Object obj)
        {
            var ret = new StringBuilder();

            var fields = new StringBuilder();
            var valueFields = new StringBuilder();
            fields.Append(GenInsertColFields(md));
            valueFields.Append(GenInsertValueFields(md));
            ret.Append(" UPDATE ");
            ret.Append(md.TableName + " SET ");
            var type = obj.GetType();
            Utility.IPropertyGetter pg;
            if (type.Equals(typeof(ExpandoObject)))
            {
                var expando = obj as ExpandoObject;
                ret.Append(GenExpandoUpdateParaString(expando));
            }
            else
            {
                ret.Append(GenNormalUpdateParaString(obj));
            }
            return ret.ToString();
        }

        private string GenExpandoUpdateParaString(ExpandoObject obj)
        {
            var ret = new StringBuilder();
            var i = 1;
            var pg = new Utility.ExpandoPorpertyGetter();
            var paraDic = pg.GetPropertyDic(obj);
            foreach (var key in paraDic.Keys)
            {
                ret.Append(" " + key + "=@" + key + " ");
                if (i < paraDic.Keys.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
            return ret.ToString();
        }

        private string GenNormalUpdateParaString(object obj)
        {
            var ret = new StringBuilder();
            var objMdf = new ModelDefinitionConverter().ConverClassToModelDefinition(obj.GetType());
            int i = 1;
            foreach (var df in objMdf.PropertyColumnDic.Values)
            {
                ret.Append(" " + df.ColumnName + "=@" + df.PropName + " ");
                if (i < objMdf.PropertyColumnDic.Values.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
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
                    ret += " ,";
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
                    ret += " ,";
                }
                i++;
            }
            return ret;
        }

    }
}
