using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.BasicDefinitions
{
    public class ColumnTypeDefinition
    {
        public Type FieldType { get; set; }
        public int Length { get; set; }
        public bool Nullable { get; set; }
        public Object DefaultValue { get; set; }
        public string Comment { get; set; }
    }
}
