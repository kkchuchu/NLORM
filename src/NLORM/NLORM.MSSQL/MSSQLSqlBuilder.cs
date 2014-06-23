using NLORM.Core;

namespace NLORM.MSSQL
{
    public class MSSQLSqlBuilder : BaseSqlBuilder
    {
		public string GenCreateTableSql<T>() where T : new()
		{
			string result = null;

			return result;
		}

		// select
		// update
		// insert
		// delete
    }
}
