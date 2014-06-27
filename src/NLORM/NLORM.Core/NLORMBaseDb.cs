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


        virtual public IEnumerable<T> Query<T>()
        {
            GenSelectSql(typeof(T));
            GenWhereSql();
            string selectStr = sqlBuilder.SQLString;
            string whereStr = sqlBuilder.GetWhereSQLString();
            string Sql = selectStr;
            if (!string.IsNullOrEmpty(whereStr))
            {
                Sql += " WHERE "+whereStr;
            }
            dynamic consObject = sqlBuilder.GetWhereParas();
            ResetFliterCache();
            return (IEnumerable<T>)Query<T>(Sql, consObject);
        }


        virtual public int Insert<T>(Object o)
        {
            string sql = sqlBuilder.GenInsertSql<T>();
            return SqlMapper.Execute(dbConnection,sql,o);
        }

        public INLORMDb FliterBy(FliterType fType, dynamic param)
        {

            FliterObjects = FliterObjects?? new List<FliterObject>();
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
            if (FliterObjects == null)
            {
                return;
            }
            foreach (var f in FliterObjects)
            {
                sqlBuilder.GenWhereCons(f);
            }
        }

        private void GenSelectSql(Type t)
        {
            QueryType = t;
            sqlBuilder.GenSelect(t);
        }

        private void ResetSqlBuilder()
        {
            sqlBuilder = sqlBuilder.CreateOne();
        }

        private void ResetFliterCache()
        {
            ResetSqlBuilder();
            FliterObjects = null;
        }



    }
}
