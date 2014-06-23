using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.SQLite;
using NLORM.Core.Attributes;
using System.IO;

namespace NLORM.SQLite.Test
{
    class TestClass
    {
        [ColumnType(DbType.String,"30",false,"0001","this is id comment")]
        public string ID { get; set; }

    }

    class TestClass2
    {
        public string ID { get; set; }

    }

    /// <summary>
    /// Summary description for NLORMSQLiteDbTest
    /// </summary>
    [TestClass]
    public class NLORMSQLiteDbTest
    {
        string connectionString;
        string filePath;
        public NLORMSQLiteDbTest()
        {
            connectionString = "Data Source="+filePath;
            filePath = "C:\\test.sqlite";
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
         //Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void TestInitialize()
        {
            try
            {
                File.Delete(filePath);
            }
            finally
            {

            }
        }
        
         //Use TestCleanup to run code after each test has run
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
        #endregion

        [TestMethod]
        public void TestCreateTable()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClass>();
        }

        [TestMethod]
        public void TestCreateTableWithoutDef()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClass2>();
        }

        [TestMethod]
        public void TestDropTable()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.DropTable<TestClass>();
        }

        [TestMethod]
        public void TestDropTableWithoutDef()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.DropTable<TestClass2>();
        }
    }
}
