using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Dapper;
using System.ComponentModel;
using System.Dynamic;

namespace NLORM.Core
{
    public class NLORMBaseDb : INLORMDb
    {
        protected ISqlBuilder sqlBuilder;
        protected IDbConnection dbConnection;

        virtual public void Open()
        {
            dbConnection.Open();
        }

        virtual public void Close()
        {
            dbConnection.Close();
        }

        virtual public void Dispose()
        {
            this.Close();
            dbConnection.Dispose();
        }

        virtual public void CreateTable<T>() where T : new()
        {
            string sql = sqlBuilder.GenCreateTableSql<T>();
            dbConnection.Execute(sql);
        }

        virtual public void DropTable<T>() where T : new()
        {
            string sql = sqlBuilder.GenDropTableSql<T>();
            dbConnection.Execute(sql);
        }

        virtual public IEnumerable<T> Query<T>(string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var ret = SqlMapper.Query<T>(dbConnection, sql,param,transaction,buffered,commandTimeout,commandType);
            return (IEnumerable<T>)ret;
        }

        virtual public int Insert<T>(Object o)
        {
            string sql = sqlBuilder.GenInsertSql<T>();
            return SqlMapper.Execute(dbConnection,sql,o);
        }

        private void EnsureConnection()
        {
            Trace.Assert(dbConnection != null); //conn can not be null
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
        }

        private dynamic ToDynamic(object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }

    }
}
