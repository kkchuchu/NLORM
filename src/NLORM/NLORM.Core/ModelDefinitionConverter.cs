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
        private static volatile ModelDefinitionConverter instance;
        private static object syncRoot = new Object();

        private ModelDefinitionConverter() { }
        /// <summary>
        /// singleton
        /// </summary>
        public static ModelDefinitionConverter Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ModelDefinitionConverter();
                    }
                }
                return instance;
            }
        }

        public ModelDefinition ConverClassToModelDefinition<T>() where T : new()
        {
            string tableName = GetTableNameByType(typeof(T));
            var fiedlDic = GetColumnFieldDefinition(typeof(T));
            var ret = new ModelDefinition(tableName, null);
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
            //TODO not done
            var propeties = classType.GetProperties();
			Dictionary<string, ColumnFieldDefinition> ret = new Dictionary<string, ColumnFieldDefinition>();
            foreach (var pro in propeties)
            {
                string proName = pro.Name;
                var columnFieldDef = GetColumnFieldDefByProprty(pro);
            }
            return null;
        }

        private ColumnFieldDefinition GetColumnFieldDefByProprty(PropertyInfo prop)
        {
            object[] attrs = prop.GetCustomAttributes(true);
            var ret = new ColumnFieldDefinition();
            foreach (object attr in attrs)
            {
                var colNameAttr = (ColumnNameAttribute)attr;
                var colTypeAttr = (ColumnTypeAttribute)attr;
                if (colNameAttr != null)
                {
                    AsignColNameAttrToDef(ret, colNameAttr);
                }
                if (colTypeAttr != null)
                {
                    AsignColTypeAttrToDef(ret, colTypeAttr);
                }
            }
            return ret;
        }

        private void AsignColNameAttrToDef(ColumnFieldDefinition colunmF,ColumnNameAttribute colNameAttr)
        {
            colunmF.ColumnName = colNameAttr.ColumnName;
        }

        private void AsignColTypeAttrToDef(ColumnFieldDefinition colunmF, ColumnTypeAttribute colTypeAttr)
        {
            colunmF.FieldType = colTypeAttr.DBType;
            colunmF.Length = colTypeAttr.Length;
            colunmF.Nullable = colTypeAttr.Nullable;
            colunmF.DefaultValue = colTypeAttr.DefaultValue;
            colunmF.Comment = colTypeAttr.Comment;
        }
    }
}
