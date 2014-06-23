using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NLORM.Core.BasicDefinitions;
using NLORM.Core.Attributes;

namespace NLORM.Core
{
    public class ModelDefinitionConverter
    {

        public ModelDefinitionConverter()
        {
        }

        public ModelDefinition ConverClassToModelDefinition<T>() where T : new()
        {
            string tableName = GetTableNameByType(typeof(T));
            var fiedlDic = GetColumnFieldDefinition(typeof(T));
            var ret = new ModelDefinition(tableName, fiedlDic);
            return ret;
        }

        private string GetTableNameByType(Type classType)
        {
            var tableNameAttr = GetTableNameAttrByType(classType);
            string ret = tableNameAttr == null ? classType.Name : tableNameAttr.TableName;
            return ret;
        }

        private TableNameAttribute GetTableNameAttrByType(Type classType)
        {
            return (TableNameAttribute)Attribute.GetCustomAttribute(classType, typeof(TableNameAttribute));
        }

        private Dictionary<string, ColumnFieldDefinition> GetColumnFieldDefinition(Type classType)
        {
            var propeties = classType.GetProperties();
			Dictionary<string, ColumnFieldDefinition> ret = new Dictionary<string, ColumnFieldDefinition>();
            foreach (var pro in propeties)
            {
                string proName = pro.Name;
                var columnFieldDef = GetColumnFieldDefByProprty(pro);

				if ( GenCloumn( pro) == true )
					ret.Add(pro.Name, columnFieldDef);
            }
            return ret;
        }

        private ColumnFieldDefinition GetColumnFieldDefByProprty(PropertyInfo prop)
        {
            object[] attrs = prop.GetCustomAttributes(true);
            var ret = new ColumnFieldDefinition();
            ColumnNameAttribute colNameAttr = null;
            ColumnTypeAttribute colTypeAttr = null; 
            foreach (object attr in attrs)
            {
                colNameAttr = attr as ColumnNameAttribute;
                colTypeAttr = attr as ColumnTypeAttribute;
            }
            AsignColNameAttrToDef(ret, colNameAttr,prop);
            AsignColTypeAttrToDef(ret, colTypeAttr,prop);
            return ret;
        }

        private void AsignColNameAttrToDef(ColumnFieldDefinition colunmF,
            ColumnNameAttribute colNameAttr,PropertyInfo prop)
        {
            colunmF.ColumnName = colNameAttr == null ? prop.Name : colNameAttr.ColumnName;
        }

        private void AsignColTypeAttrToDef(ColumnFieldDefinition colunmF, 
            ColumnTypeAttribute colTypeAttr,PropertyInfo prop)
        {
            if (colTypeAttr != null)
            {
                colunmF.FieldType = colTypeAttr.DBType;
                colunmF.Length = colTypeAttr.Length;
                colunmF.Nullable = colTypeAttr.Nullable;
                colunmF.DefaultValue = colTypeAttr.DefaultValue;
                colunmF.Comment = colTypeAttr.Comment;
            }
            else
            {
                colunmF.FieldType = Dapper.SqlMapper.LookupDbType(prop.PropertyType,prop.Name);
            }
        }

		private bool GenCloumn(PropertyInfo prop)
		{
			bool tag = true;

			object[] attrs = prop.GetCustomAttributes( true);

			NotGenColumnAttribute ngattr = null;
			foreach ( object attr in attrs)
			{
				ngattr = attr as NotGenColumnAttribute;
				if ( ngattr != null && ngattr.GenCol == false )
					tag = false;
			}

			return tag;
		}
    }
}
