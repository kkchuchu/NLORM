using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : BaseAttribute
    {
        public ColumnNameAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
        public string ColumnName { get; set; }
    }
 }
