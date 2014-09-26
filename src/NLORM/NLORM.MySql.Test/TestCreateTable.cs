using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core.Attributes;
using System.Data;

namespace NLORM.MySql.Test
{
    /// <summary>
    /// Summary description for TestCreateTable
    /// </summary>
    [TestClass]
    public class TestCreateTable
    {
        static public string connectionString ;
        public TestCreateTable()
        {
            connectionString = "Server=test.mysql.nlorm;Database=nlorm;uid=admin;pwd=1qaz;";
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

        class TestClassOnlyString
        {
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyString()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyString>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyString>();
        }

        class TestClassOnlyStringWCfd
        {
            [ColumnType(DbType.String, "30", false, "this is id comment")]
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyStringWithCfd()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyStringWCfd>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyStringWCfd>();
        }

        class TestClassOnlyInt
        {
            public int ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt()
        {
            try
            {
                var Dbc = new NLORMMySqlDb(connectionString);
                Dbc.DropTable<TestClassOnlyInt>();
            }
            catch { }
            var MySqlDbc = new NLORMMySqlDb(connectionString);
            MySqlDbc.CreateTable<TestClassOnlyInt>();
        }

    }
}
