using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.Core.Test
{
	class Testclass01
	{
		public string ID { get; set;}
	}
	public class NLAdapterTest
	{
		[TestMethod]
		public void TestCallNLapter()
		{
			INLORMDb db = new NLORMBaseDb();
			string selectstr = @"SELECT * FROM ";
			NLAdapter<Testclass01> adapter = new NLAdapter<Testclass01>( db, selectstr);
//			adapter
		}
	}
}
