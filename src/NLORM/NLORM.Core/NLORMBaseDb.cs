﻿using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using NLORM.Core.BasicDefinitions;

namespace NLORM.Core
{
    public class NLORMBaseDb : INLORMDb
    {
        protected ISqlBuilder SqlBuilder;
        protected IDbConnection DbConnection;
        protected Type QueryType;
        protected List<FliterObject> FliterObjects;

        virtual public void Open()
        {
            DbConnection.Open();
        }

        virtual public void Close()
        {
            DbConnection.Close();
        }

        virtual public void Dispose()
        {
            Close();
            DbConnection.Dispose();
        }

        virtual public void CreateTable<T>() where T : new()
        {
            var sql = SqlBuilder.GenCreateTableSql<T>();
            DbConnection.Execute(sql);
        }

        virtual public void DropTable<T>() where T : new()
        {
            var sql = SqlBuilder.GenDropTableSql<T>();
            DbConnection.Execute(sql);
        }

        virtual public IEnumerable<T> Query<T>(string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var ret = SqlMapper.Query<T>(DbConnection, sql,param,transaction,buffered,commandTimeout,commandType);
            return (IEnumerable<T>)ret;
        }


        virtual public IEnumerable<T> Query<T>()
        {
            GenSelectSql(typeof(T));
            GenWhereSql();
            var selectStr = SqlBuilder.SQLString;
            var whereStr = SqlBuilder.GetWhereSQLString();
            var sql = selectStr;
            if (!string.IsNullOrEmpty(whereStr))
            {
                sql += " WHERE "+whereStr;
            }
            var consObject = SqlBuilder.GetWhereParas();
            ResetFliterCache();
            return (IEnumerable<T>)Query<T>(sql, consObject);
        }


        virtual public int Insert<T>(Object o)
        {
            var sql = SqlBuilder.GenInsertSql<T>();
            return SqlMapper.Execute(DbConnection,sql,o);
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

        private void GenWhereSql()
        {
            if (FliterObjects == null)
            {
                return;
            }
            foreach (var f in FliterObjects)
            {
                SqlBuilder.GenWhereCons(f);
            }
        }

        private void GenSelectSql(Type t)
        {
            QueryType = t;
            SqlBuilder.GenSelect(t);
        }

        private void ResetSqlBuilder()
        {
            SqlBuilder = SqlBuilder.CreateOne();
        }

        private void ResetFliterCache()
        {
            ResetSqlBuilder();
            FliterObjects = null;
        }



    }
}
