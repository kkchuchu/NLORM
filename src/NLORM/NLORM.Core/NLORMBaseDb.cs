using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Dapper;
using System.ComponentModel;
using System.Dynamic;
using NLORM.Core.BasicDefinitions;

namespace NLORM.Core
{
    public class NLORMBaseDb : INLORMDb
    {
        protected ISqlBuilder sqlBuilder;
        protected IDbConnection dbConnection;
        protected Type QueryType;
        protected List<FliterObject> FliterObjects;

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

        virtual public INLORMDb Find(Type t)
        {
            QueryType = t;
            FliterObjects = new List<FliterObject>();
            sqlBuilder.GenSelect(t);
            return this;
        }

        virtual public IEnumerable<T> Query<T>()
        {
            GenWhereSql();
            string selectStr = sqlBuilder.SQLString;
            string whereStr = sqlBuilder.GetWhereSQLString();
            string Sql = selectStr + " WHERE " + whereStr;
            dynamic consObject = sqlBuilder.GetWhereParas();
            //TODO Not Yet imp Fliter By
            return (IEnumerable<T>)Query<T>(Sql, consObject);
        }


        virtual public int Insert<T>(Object o)
        {
            string sql = sqlBuilder.GenInsertSql<T>();
            return SqlMapper.Execute(dbConnection,sql,o);
        }

        public INLORMDb FliterBy(FliterType fType, dynamic param)
        {
            var f = new FliterObject();
            f.ClassType = QueryType;
            f.Cons = param;
            f.Fliter = fType;
            FliterObjects.Add(f);
            return this;
        }

        private void EnsureConnection()
        {
            Trace.Assert(dbConnection != null); //conn can not be null
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
        }

        private void GenWhereSql()
        {
            foreach (var f in FliterObjects)
            {
                sqlBuilder.GenWhereCons(f);
            }
        }


    }
}
