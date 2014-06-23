using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;

namespace NLORM.Core
{
    public class NLORMBaseDb 
    {
        protected ISqlBuilder sqlBuilder;
        protected IDbConnection dbConnection;

        public void Open()
        {
            dbConnection.Open();
        }

        public void Close()
        {
            dbConnection.Close();
        }

        public void Dispose()
        {
            this.Close();
            dbConnection.Dispose();
        }

        public void CreateTable<T>() where T : new()
        {
            string sql = sqlBuilder.GenCreateTableSql<T>();
            dbConnection.Execute(sql);
        }



    }
}
