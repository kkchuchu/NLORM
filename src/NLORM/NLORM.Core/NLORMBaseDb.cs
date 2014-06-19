using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NLORM.Core
{
    public class NLORMBaseDb 
    {
        protected ISqlBuilder sqlBuilder;
        protected IDbConnection dbc;

        public void Open()
        {
            dbc.Open();
        }

        public void Close()
        {
            dbc.Close();
        }

        public void CreateTable<T>() where T : new()
        {
            string sql = sqlBuilder.GenCreateTableSql<T>();
        }



    }
}
