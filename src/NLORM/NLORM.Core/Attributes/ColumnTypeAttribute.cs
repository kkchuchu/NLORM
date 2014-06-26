﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NLORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnTypeAttribute : BaseAttribute
    {
        public ColumnTypeAttribute(DbType dbtype,string length,bool nullable,string comment)
        {
            this.DBType = dbtype;
            this.Length = length;
            this.Nullable = nullable;
            this.Comment = comment;
        }
        public DbType DBType { get; set; }
        public string Length { get; set; }
        public bool Nullable { get; set; }
        public string Comment { get; set; }
    }
}
