﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NLORM.Core.BasicDefinitions;

namespace NLORM.Core
{
    public interface ISqlGenerator
    {
        string GenCreateTableSql(ColumnFieldDefinition cfd);
    }
}