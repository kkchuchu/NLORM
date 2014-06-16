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
            TableNameAttribute attrTableName =
          (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));

            var ret = new ModelDefinition(attrTableName.TableName, null);

            return ret;
        }
    }
}
