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
    class TestClass01
    {
		[ColumnType(DbType.String,"30",false,"this is id comment")]
        public string ID { get; set; }

		public string name { get; set; }
    }

	class TestClass2
    {
        public string ID { get; set; }
    }

	[TestClass]
	public class NLORMSSQLDbTest
    {
		private string constr = @"Data Source=.\SQLEXPRESS;Database=TestORM;Integrated Security=True;";
		[TestInitialize()]
		public void TestInitialize()
		{
			INLORMDb msdb = null;
			try
			{
				msdb = new NLORMMSSQLDb( constr);
				msdb.DropTable<TestClass01>();
				msdb.DropTable<TestClass2>();
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
			var result = db.Find( typeof(TestClass01) ).FliterBy( FliterType.EQUAL_AND, new { name = "albert"} ).Query<TestClass01>();

			Assert.AreEqual( 1, result.Count() );
		}

		[TestMethod]
		public void TestNewQueryMethod()
		{
			INLORMDb db = new NLORMMSSQLDb( constr);
			db.CreateTable<TestClass01>();
						
			var items = db.Query<TestClass01>() as List<TestClass01>;
			//NLAdapter adapter = new NLAdapter();
			// adapter.INLORMDb = db;
			// db.query(@"select * from tablename");
			// db.items.add ( insert)
			// db.items[4].name = "rick";			
			// db.update;
		}

		public void test()
		{
			this.JoinTest<TestClass01>( x => x.ID == x.
		}

		private void JoinTest<T>( Func<T,bool> filter)
		{
			filter();
		}
	}

	public class NLAdapter
	{
		public INLORMDb db { get; set;}

		private string _selectstr;
		public string Select
		{
			get
			{
				return _selectstr;
			}
			set
			{
				this._selectstr = value;
			}
		}
		
		public NLAdapter( INLORMDb db, string selectstring)
		{
			this._selectstr = selectstring;
			this.db = db;
		}

		public IEnumerable<T> Fill()
		{
			SqlMapper mapping
		}
	}
}