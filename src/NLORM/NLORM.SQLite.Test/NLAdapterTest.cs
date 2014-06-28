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
	class Testclass01
	{
		public string ID { get; set;}
	}
	[TestClass]
	public class NLAdapterTest
	{
		[TestCleanup()]
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
			INLORMDb db = new SQLite.NLORMSQLiteDb( connectionString);			
			try
			{
				db.DropTable<Testclass01>();
			}
			catch( Exception)
			{
			}
			db.CreateTable<Testclass01>();
			string selectstr = @"SELECT * FROM Testclass01";
			NLORM.Core.NLAdapter<Testclass01> adapter = new NLORM.Core.NLAdapter<Testclass01>( db, selectstr);
			adapter.Collection.Add( new Testclass01(){ ID = @"10"} );
			adapter.Collection.Add( new Testclass01(){ ID = @"20"} );// if add same items, what's the result.
			adapter.Collection.Remove( new Testclass01(){ ID = @"10"});
			adapter.Collection[0].ID = @"4";

			foreach ( Testclass01 item in adapter.Collection)
			{
				
			}
		}
	}
}
