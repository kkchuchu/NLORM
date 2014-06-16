using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.BasicDefinitions
{
    public class ColumnFieldDefinition
    {
        public string ColumnName { get; set; }
        public Type FieldType { get; set; }
        public string Length { get; set; }
        public bool Nullable { get; set; }
        public Object DefaultValue { get; set; }
        public string Comment { get; set; }
    }
}
