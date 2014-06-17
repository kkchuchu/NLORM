using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using NLORM.Core;

namespace NLORM.SQLite
{
    public class NLSQLiteConnection : INLORMDbConnection
    {
        IDbConnection dbc;

        public NLSQLiteConnection(string connectionString)
        {
            dbc = new SQLiteConnection(connectionString);
        }

        public bool CreateTable<T>()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            dbc.Open();
        }

        public void Close()
        {
            dbc.Close();
        }

        public void Dispose()
        {
            dbc.Dispose();
        }
    }
}
