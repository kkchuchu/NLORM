using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.MySql;
using NLORM.Core;
using NLORM.Core.Attributes;


namespace NLORM.MySql.Test
{
    class TestClass
    {
        [ColumnType(DbType.String, "30", false, "0001", "this is id comment")]
        public string ID { get; set; }

    }

    class TestClass2
    {
        public string ID { get; set; }

    }

    /// <summary>
    /// Summary description for MySqlDbTest
    /// </summary>
    [TestClass]
    public class MySqlDbTest
    {
        public MySqlDbTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestConnect()
        {
            string connectionString = "Server=sql5.freemysqlhosting.net;Database=sql544630;uid=sql544630;pwd=mD3%bW7*;";
            var db = new NLORMMySqlDb( connectionString);
            db.Open();
        }

        [TestMethod]
        public void TestCreateTable()
        {
            string connectionString = "Server=sql5.freemysqlhosting.net;Database=sql544630;uid=sql544630;pwd=mD3%bW7*;";
            var db = new NLORMMySqlDb(connectionString);
            db.CreateTable<TestClass>();
        }
    }
}
