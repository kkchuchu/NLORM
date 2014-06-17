using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
    public interface INLORMDbConnection
    {
        bool CreateTable<T>();
    }
}
