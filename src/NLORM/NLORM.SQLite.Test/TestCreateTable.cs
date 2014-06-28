using System;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core.Attributes;

namespace NLORM.SQLite.Test
{
    /// <summary>
    /// Summary description for TestCreateTable
    /// </summary>
    [TestClass]
    public class TestCreateTable
    {
        string connectionString;
        private static string filePath;
        public TestCreateTable()
        {
            filePath = "C:\\test.sqlite";
            connectionString = "Data Source=" + filePath;
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

        class TestClassOnlyString
        {
            public string ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyString()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyString>();
        }

        class TestClassOnlyStringWCfd
        {
            [ColumnType(DbType.String, "30", false, "this is id comment")]
            public string ID { get; set; }
        }


        [TestMethod]
        public void TestCreateTableOnlyStringWithCfd()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyStringWCfd>();
        }

        class TestClassOnlyInt
        {
            public int ID { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyInt()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyInt>();
        }

        class TestClassOnlyIntWCfd
        {
            [ColumnType(DbType.Int32, "", false, "this is id comment")]
            public int ID { get; set; }

            [ColumnType(DbType.Int16, "", false, "this is id comment")]
            public int ID2 { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyIntWcfd()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyIntWCfd>();
        }

        class TestClassOnlyDateTime
        {
            public DateTime time { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyDateTime()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyDateTime>();
        }

        class TestClassOnlyDateTimeWCfd
        {
            [ColumnType(DbType.DateTime, "", false, "this is time comment")]
            public DateTime time { get; set; }
        }

        [TestMethod]
        public void TestCreateTableOnlyDateTimeWcfd()
        {
            var sqliteDbc = new NLORMSQLiteDb(connectionString);
            sqliteDbc.CreateTable<TestClassOnlyDateTimeWCfd>();
        }

    }
}
