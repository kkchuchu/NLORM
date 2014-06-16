using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.BasicDefinitions
{
    public class ModelDefinition
    {
        public string TableName { get { return this.tableName; } }
        public Dictionary<string, ColumnFieldDefinition> PropertyColumnDic;
        private string tableName;
        public ModelDefinition(string tablename, Dictionary<string, ColumnFieldDefinition> propertyColumnDic)
        {
            tableName = tablename;
            PropertyColumnDic = propertyColumnDic;
        }
        
    }
}
