using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.BasicDefinitions
{
    public class ModelDefinition
    {
        public string TableName { get { return this.tableName; } }
        public Dictionary<string, ColumnFieldDefinition> PropertyColumnDic{get{return this.propertyColumnDic;}}
        private Dictionary<string, ColumnFieldDefinition> propertyColumnDic;
        private string tableName;
        public ModelDefinition(string tablename, Dictionary<string, ColumnFieldDefinition> propertycolumnDic)
        {
            tableName = tablename;
            propertyColumnDic = propertycolumnDic;
        }
        
    }
}
