﻿using System;
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
        public string connectionString ;
        public MySqlDbTest()
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

        [TestMethod]
        public void TestConnect()
        {
            
            var db = new NLORMMySqlDb( connectionString);
            db.Open();
        }

        [TestMethod]
        public void TestCreateTable()
        {
            var db = new NLORMMySqlDb(connectionString);
            db.CreateTable<TestClass>();
            db.Close();
        }


        [TestMethod]
        public void TestDropTable()
        {
            var db = new NLORMMySqlDb(connectionString);
            db.DropTable<TestClass>();
            db.Close();
        }

        [TestMethod]
        public void TestCreateTableWithoutAttr()
        {
            var db = new NLORMMySqlDb(connectionString);
            db.CreateTable<TestClass2>();
            db.Close();
        }

        [TestMethod]
        public void TestDropTableWithoutAttr()
        {
            var db = new NLORMMySqlDb(connectionString);
            db.DropTable<TestClass2>();
            db.Close();
        }
    }
}
