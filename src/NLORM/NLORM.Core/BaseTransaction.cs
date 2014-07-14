using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
    public class BaseTransaction : ITransaction
    {
        private IDbTransaction trans;

        public BaseTransaction(IDbConnection dbc)
        {
            trans = dbc.BeginTransaction();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
