using NLORM.Core;
using System.Data.SqlClient;

namespace NLORM.MSSQL
{
	public class NLORMMSSQLDb : NLORMBaseDb
	{
		public NLORMMSSQLDb( string connectionString)
		{
			this.dbc = new SqlConnection( connectionString);
			this.sqlBuilder = new MSSQLSqlBuilder();
		}
	}
}
