using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NLORM.LambdaExtension;

namespace NLORM.LambdaExtension
{
    public class NEDataBase
    {
        public NEDataBase( string connectionstring)
        {
            this.db = new MSSQL.NLORMMSSQLDb( connectionstring);

            visitor = new NExpressionVisitor();
            list = new List<Expression>();
        }
        private NExpressionVisitor visitor;
        private INLORMDb db;
        private List<Expression> list;

        public NEDataBase Where<T>( Expression<Func<T, bool>> exp)
        {
            var e = visitor.Visit(exp);
            
            list.Add(e);
            return this;
        }

        public List<object> Select<T> ( Expression<Func<T, object>> exp)
        {
            
            list.Clear();
            return new List<object>();
        }

        public bool Insert<T> ( T t)
        {
            return false;
        }

        public int Update<T>( object t)
        {
            return 0;
        }

        public int Delete<T>()
        {
            return 0;
        }

    }

    public class PartialSQL
    {
        
    }
}