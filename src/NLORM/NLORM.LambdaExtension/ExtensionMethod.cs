using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.LambdaExtension
{
    public partial interface INLORMDb
    {
        INLORMDb Where<T>( Expression<Func<T, bool>> exp);

        List<object> Select<T>( Expression<Func<T, object>> exp);

        bool Insert<T>( T t);

        int Update<T>( object t);

        int Delete<T>();
    }    
}
