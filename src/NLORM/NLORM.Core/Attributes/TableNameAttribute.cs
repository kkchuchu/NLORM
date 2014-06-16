using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : BaseAttribute
    {
        public TableNameAttribute(string tableName)
        {
            this.TableName = tableName;
        }
        public string TableName { get; set; }
    }
}
