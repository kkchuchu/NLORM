using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using NLORM.Core.Attributes;
using System.Data;

namespace NLORM.MSSQL.Test
{
    class TestClass01
    {
		[ColumnType(DbType.String,"30",false,"0001","this is id comment")]
        public string ID { get; set; }
    }

	[TestClass]
	public class NLORMSSQLDbTest
    {
		[TestMethod]
        public void TestCreateTable()
        {
            //NLORMBaseDb mssqlDb = new NLORMMSSQLDb("Data Source=C:\\test.sqlite");
            //mssqlDb.CreateTable<TestClass01>();
        }
	}
}
