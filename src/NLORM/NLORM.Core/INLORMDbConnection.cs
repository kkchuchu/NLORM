using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
    public interface INLORMDbConnection
    {

        void Open();
        void Close();
        void Dispose();

        bool CreateTable<T>();
    }
}
