using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;


namespace NLORM.SQLite.Test
{
    /// <summary>
    /// Summary description for TestUpdate
    /// </summary>
    [TestClass]
    public class TestUpdate
    {
        string connectionString;
        private static string filePath;
        public TestUpdate()
        {
            filePath = "C:\\test.sqlite";
            connectionString = "Data Source=" + filePath;
        }

        class TestClassOne
        {
            public string Id { get; set;}

            public int income { get; set;}
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            try
            {
                File.Delete(filePath);
                var db = this.createtable();
                this.insertdata( db);
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
        private INLORMDb createtable()
        {
            var db = new NLORMSQLiteDb(connectionString);
            db.CreateTable<TestClassOne>();
            return db;
        }
        private void insertdata(INLORMDb db)
        {
            db.Insert<TestClassOne>(new TestClassOne() { Id = "sssss", income = 123456 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "rrrrr", income = 789012 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "fffff", income = 345678 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "lllll", income = 901234 });
            db.Insert<TestClassOne>(new TestClassOne() { Id = "alber", income = 901234 });
        }

        [TestMethod]
        public void TestUpdateOneRow()
        {
            var db = new NLORMSQLiteDb(connectionString);
            var newobj = new TestClassOne { Id = "sssss", income = 100 };
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Update<TestClassOne>(newobj);
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>().First();
            Assert.AreEqual(100, items.income);
        }

        [TestMethod]
        public void TestUpdateWithOr()
        {
            var db = new NLORMSQLiteDb(connectionString);
            var newobj = new TestClassOne {  income = 100 };
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" })
                .Or().FilterBy(FilterType.EQUAL_AND, new { Id = "rrrrr" })
                .Update<TestClassOne>(new { income = 100 });
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>().First();
            var itemsr = db.FilterBy(FilterType.EQUAL_AND, new { Id = "rrrrr" }).Query<TestClassOne>().First();
            Assert.AreEqual(100, items.income);
            Assert.AreEqual(100, itemsr.income);
        }

    }
}
