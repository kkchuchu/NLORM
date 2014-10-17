using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLORM.LambdaExtension;
using NLORM.Core;
using NLORM.MSSQL;

namespace NLORM.LambdaExtension.Test
{
    class Model
    {
        public string ID { get; set;}

        public string NAME { get; set;}
    }

    [TestClass]
    public class NLORMLambdaTest
    {
        static readonly string connectionstring = @"";
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {
            
        }

        [TestMethod]
        public void TestCallMethd()
        {
            NEDataBase db = new NEDataBase(connectionstring);
            db.Where<Model>( x => x.NAME == "albert" ).Select<Model>( x => new { x.ID, x.NAME});
            
            db.Insert<Model>( new Model(){ ID = "111", NAME = "azzz"});
            db.Where<Model>( x => x.ID == "123" && x.ID == "456").Delete<Model>();
            db.Where<Model>( x => x.ID == "asdf").Update<Model>( new { ID = "123", NAME = "aooqwe"});
        }
    }
}
