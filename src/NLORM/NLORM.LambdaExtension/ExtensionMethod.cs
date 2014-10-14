using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.LambdaExtension
{
    public class ExtensionMethod
    {
        public static MSSQL.NLORMMSSQLDb Where<T>( this MSSQL.NLORMMSSQLDb db, Expression<Func<T, bool>> exp)
        {

            return db;
        }

        public static List<object> Select<T>( this INLORMDb db, Expression<Func<T, object>> exp)
        {
            return new List<object>();
        }

        public static bool Insert<T>( this INLORMDb db, T t)
        {
            return false;
        }

        public static int Update<T>( this INLORMDb db, object t)
        {
            return 0;
        }

        public static int Delete<T>( this INLORMDb db)
        {
            return 0;
        }
    }
}
