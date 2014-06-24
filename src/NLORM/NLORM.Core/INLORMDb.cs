using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NLORM.Core
{
    public interface INLORMDb
    {
        void Open();
        void Close();
        void Dispose();

        void CreateTable<T>() where T : new();
        void DropTable<T>() where T : new();
        IEnumerable<T> Query<T>(string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        int Insert<T>(Object o);

    }
}
