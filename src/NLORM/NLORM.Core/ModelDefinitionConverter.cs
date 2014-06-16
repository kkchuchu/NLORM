using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return null;
        }
    }
}
