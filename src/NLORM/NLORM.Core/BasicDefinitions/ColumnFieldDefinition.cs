using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.BasicDefinitions
{
    public class ColumnFieldDefinition
    {
        public string ColumnName { get; set; }
        public ColumnTypeDefinition ColumnType { get; set; }
    }
}
