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
		private string connectionString;
		public NLAdapterTest()
		{
			string filePath = "C:\\test.sqlite";
            connectionString = "Data Source="+filePath;
		}
		[TestMethod]
		public void TestCallNLapter()
		{
			INLORMDb db = new SQLite.NLORMSQLiteDb( connectionString);
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
