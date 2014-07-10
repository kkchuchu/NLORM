using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using NLORM.Core.Attributes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Dapper;

namespace NLORM.MSSQL.Test
{

	[TestClass]
	public class NLORMSSQLDbTest
    {
        class TestClass01
        {
            [ColumnType(DbType.String, "30", false, "this is id comment")]
            public string ID { get; set; }

            public string name { get; set; }
        }

        class TestClassDecimal
        {
            [ColumnType(DbType.Decimal, "20,10", false, "decimal test")]
            public Int32 DID { get; set; }

            [ColumnType(DbType.Decimal, "10", true, "decimal test")]
            public Int32 DDID { get; set; }
        }
        class TestClass2
        {
            public string ID { get; set; }
        }

        class TestClassbit
        {
            [ColumnType(DbType.Boolean, "1", false, "bit test")]
            public Boolean BTAG { get; set; }

            [ColumnType(DbType.Boolean, "1", true, "bit test")]
            public Boolean BBTAG { get; set; }
        }
        public static string coonectionstring = @"Data Source=.\SQLEXPRESS;Database=TestORM;Integrated Security=True;";
        private string constr = coonectionstring;
		[TestInitialize()]
		public void TestInitialize()
		{
			INLORMDb msdb = null;
			try
			{
				msdb = new NLORMMSSQLDb( constr);
				msdb.DropTable<TestClass01>();
				msdb.DropTable<TestClass2>();
				msdb.DropTable<TestClassDecimal>();
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
				mssqlDb.DropTable<TestClassDecimal>();
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
			for ( int i = 0; i < 1000; i++)
			{
				db.Insert<TestClass01>( new TestClass01(){ ID = @"0" + i.ToString(), name = @"00" + i.ToString() });
			}
			
			var result = db.Query<TestClass01>(@"SELECT * FROM TestClass01");
			Assert.AreEqual( result.Count(), 1000);
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
		public void TestQueryMethod1()
		{
			INLORMDb db = null;
			db = new NLORMMSSQLDb( constr);
			this.TestInsertAlotItems();
			var result = db.Query<TestClass01>( @"SELECT * FROM TestClass01 where ID = @ID", new TestClass01(){ ID = @"01"});
			Assert.AreEqual( 1, result.Count());
		}

		[TestMethod]
		public void TestQueryMethod2()
		{
			INLORMDb db = new NLORMMSSQLDb( constr);
			db.CreateTable<TestClass01>();
			db.Insert<TestClass01>( new TestClass01(){ ID = @"11", name = @"albert"});
			db.Insert<TestClass01>( new TestClass01(){ ID = @"22", name = @"star"});
			var result = db.FliterBy( FliterType.EQUAL_AND, new { name = "albert"} ).Query<TestClass01>();

			Assert.AreEqual( 1, result.Count() );
		}

		[TestMethod]
		public void TestCreatDecimalType()
		{
			INLORMDb db = new NLORMMSSQLDb( constr);
			db.CreateTable<TestClassDecimal>();

			db.DropTable<TestClassDecimal>();
		}

		[TestMethod]
		public void TestCreatbitType()
		{
			INLORMDb db = new NLORMMSSQLDb( constr);
			db.CreateTable<TestClassbit>();

			db.DropTable<TestClassbit>();
		}
	}
}