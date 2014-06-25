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
		[ColumnType(DbType.String,"30",false,"0001","this is id comment")]
        public string ID { get; set; }
    }

	public class APPLY
	{
		string APPLICATION_ID;
		string SEQ_NO;
		string STEP;
		DateTime CREATE_DT;
		string CREATE_USER;	
		string URGENT_FLAG;	
		string DATA_XML;//XML	
		string STATUS;
		string STATUS_STEP;	
		string REJECT_GROUP;
		string REJECT_CODE;	
		DateTime RETECT_DTE;	
		string APPLY_USER;	
		string POST_RESULT;	
		DateTime MNT_DT;
		string MNT_USER;
	}

	[TestClass]
	public class NLORMSSQLDbTest
    {
		private string constr = @"Data Source=192.168.95.106;Initial Catalog=CARD_ALBERT;User ID=CyberCArd;Password=Passw0rd";

		[TestInitialize()]
		public void TestInitialize()
		{
			INLORMDb mssqlDb = null;
			try
			{
				mssqlDb = new NLORMMSSQLDb( constr);
				mssqlDb.DropTable<TestClass01>();
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
			}
			catch ( Exception)
			{
			}
		}

		[TestMethod]
        public void TestCreateTable()
        {
			INLORMDb mssqlDb = null;
			try
			{
				mssqlDb = new NLORMMSSQLDb( constr);
				mssqlDb.CreateTable<TestClass01>();				
			}
			catch ( SqlException)
			{
				Assert.Fail();
			}
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
