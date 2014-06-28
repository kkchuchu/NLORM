using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.SQLite;
using NLORM.Core;
using NLORM.Core.Attributes;
using NLORM.Core.BasicDefinitions;
using System.IO;


namespace NLORM.SQLite.Test
{
	class TestClass
    {
        [ColumnType(DbType.String,"30",false,"this is id comment")]
        public string ID { get; set; }

    }

	[TestClass]
	public class NLAdapterTest
	{
		[TestInitialize]
		public void TestInit()
		{			
            try
            {
                File.Delete(filePath);
            }
            finally
            {

            }
		}
		[TestCleanup]
        public void TestCleanup()
        {
            try
            {
                File.Delete(filePath);
            }
            finally
            {

            }
        }
		private string connectionString;
		private string filePath;
		public NLAdapterTest()
		{
			filePath = "C:\\test.sqlite";
            connectionString = "Data Source="+filePath;
		}
		[TestMethod]
		public void TestCallNLapter()
		{
			var db = new SQLite.NLORMSQLiteDb( connectionString);
			db.CreateTable<TestClass>();
			db.Insert<TestClass>( new TestClass() { ID = @"100"} );
			db.Insert<TestClass> ( new TestClass() { ID= @"10"});
			string selectstr = @"SELECT * FROM " + typeof(TestClass).Name;
			NLORM.Core.NLAdapter<TestClass> adapter = new NLORM.Core.NLAdapter<TestClass>( db, selectstr);
			adapter.Collection.Add( new TestClass(){ ID = @"20"} );// if add same items, what's the result
			adapter.Collection.Remove( new TestClass(){ ID = @"10"});
			adapter.Collection[0].ID = @"4";

			foreach ( TestClass item in adapter.Collection)
			{
				
			}

			adapter.Collection[0].ID = @"";
		}
	}
}
