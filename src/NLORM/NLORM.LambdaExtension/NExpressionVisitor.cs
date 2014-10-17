using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.LambdaExtension
{
    public class NExpressionVisitor : ExpressionVisitor
    {
        public override System.Linq.Expressions.Expression Visit(System.Linq.Expressions.Expression exp)
        {
            return base.Visit(exp);
        }
    }
}
