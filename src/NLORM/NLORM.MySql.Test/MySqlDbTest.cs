﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.MySql;


namespace NLORM.MySql.Test
{
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
            string connectionString = "Driver={MySQL ODBC 5.1 Driver};Server=sql5.freemysqlhosting.net;Database=sql544630; User=sql544630;Password=mD3%bW7*;Option=3;";
            var db = new NLORMMySqlDb( connectionString);
            db.Open();

        }
    }
}