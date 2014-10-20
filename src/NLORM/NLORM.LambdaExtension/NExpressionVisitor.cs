using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.LambdaExtension
{
    public class NExpressionVisitor : ExpressionVisitor
    {
        private ISqlBuilder sqlbuilder;
        public NExpressionVisitor( ISqlBuilder sqlbuilder)
        {
            this.sqlbuilder = sqlbuilder;
        }
        public override System.Linq.Expressions.Expression Visit(System.Linq.Expressions.Expression exp)
        {
            return base.Visit(exp);
        }
    }
}
