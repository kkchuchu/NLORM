using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using NLORM.Core.Attributes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace NLORM.MSSQL.Test
{
    class TestClass01
    {
		[ColumnType(DbType.String,"30",false,"this is id comment")]
        public string ID { get; set; }
    }

	class TestClass2
    {
        public string ID { get; set; }
    }

	[TestClass]
	public class NLORMSSQLDbTest
    {
		private string constr = @"Data Source=test.mssql.nlorm\SQLEXPRESS;Database=TestORM;Trusted_Connection=True;";

		[TestInitialize()]
		public void TestInitialize()
		{
			INLORMDb mssqlDb = null;
			try
			{
				mssqlDb = new NLORMMSSQLDb( constr);
				mssqlDb.DropTable<TestClass01>();
				mssqlDb.DropTable<TestClass2>();
			}
			catch ( Exception)
			{
			}
		}

		[TestCleanup()]
		public void TestCleanup()
		{
			INLORMDb mssqlDb = null;
			try
			{
				mssqlDb = new NLORMMSSQLDb( constr);
				mssqlDb.DropTable<TestClass01>();
				mssqlDb.DropTable<TestClass2>();
			}
			catch ( Exception)
			{
			}
		}

		[TestMethod]
        public void TestCreateTableWithoutDef()
        {
			INLORMDb mssqlDb = null;
			mssqlDb = new NLORMMSSQLDb( constr);
			mssqlDb.CreateTable<TestClass2>();
        }

		[TestMethod]
		public void TestCreateTable()
		{
			INLORMDb db = new NLORMMSSQLDb( constr);
			db.CreateTable<TestClass01>();
		}

		[TestMethod]
		public void TestInsertAlotItems()
		{
			INLORMDb db = new NLORMMSSQLDb( constr);
			db.CreateTable<TestClass01>();
			db.Insert<TestClass01>( new TestClass01(){ ID = "01"});
		}

		[TestMethod]
		public void TestDropTable()
		{
			INLORMDb db = null;
			db = new NLORMMSSQLDb( constr);
			db.CreateTable<TestClass01>();
			db.DropTable<TestClass01>();
		}

		[TestMethod]
		public void TestQuery()
		{
			INLORMDb db = null;
			db = new NLORMMSSQLDb( constr);
			db.CreateTable<TestClass01>();
			db.Insert<TestClass01>( new TestClass01(){ ID = "001"} );
		}
	}
}
