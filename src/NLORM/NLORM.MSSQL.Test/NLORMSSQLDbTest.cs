﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using NLORM.Core.Attributes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace NLORM.MSSQL.Test
{
    class TestClass01
    {
		[ColumnType(DbType.String,"30",false,"0001","this is id comment")]
        public string ID { get; set; }
    }

	class TestClass2
    {
        public string ID { get; set; }
    }

	[TestClass]
	public class NLORMSSQLDbTest
    {//"Data Source=.;Initial Catalog=tempdb;Integrated Security=True"
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
				db.Insert<TestClass01>( new TestClass01(){ ID = @"0" + i.ToString() });
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
		public void TestQuery()
		{
			INLORMDb db = null;
			db = new NLORMMSSQLDb( constr);
			this.TestInsertAlotItems();
			var result = db.Query<TestClass01>( @"SELECT * FROM TestClass01 where ID = @ID", new TestClass01(){ ID = @"01"});
			Assert.AreEqual( 1, result.Count());
		}
	}
}
