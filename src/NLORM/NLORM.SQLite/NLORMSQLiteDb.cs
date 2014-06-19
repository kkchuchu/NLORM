using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using NLORM.Core;

namespace NLORM.SQLite
{
    public class NLORMSQLiteDb : NLORMBaseDb
    {
        public NLORMSQLiteDb(string connectionString)
        {
            dbc = new SQLiteConnection(connectionString);
            sqlBuilder = new SQLiteSqlBuilder();
        }
    }
}
