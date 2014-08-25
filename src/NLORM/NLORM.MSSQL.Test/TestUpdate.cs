using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.MSSQL.Test
{
    [TestClass]
    public class TestUpdate
    {
        string connectionString = NLORMSSQLDbTest.ConnectionString;
        public TestUpdate()
        {
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
                var db = new NLORMMSSQLDb(connectionString);
                new NLORMMSSQLDb(connectionString);
                this.createtable( db);
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
                var db = new NLORMMSSQLDb(connectionString);
                db.DropTable<TestClassOne>();
            }
            finally
            {
            }
        }
        private void createtable(INLORMDb db)
        {
            try
            {
                //db.DropTable<TestClassOne>();
                db.CreateTable<TestClassOne>();
            }
            catch (Exception)
            {
            }
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
            var db = new NLORMMSSQLDb(connectionString);
            var newobj = new TestClassOne { Id = "sssss", income = 100 };
            int i = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Update<TestClassOne>(newobj);
            var items = db.FilterBy(FilterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>().First();
            Assert.AreEqual(100, items.income);
        }
    }
}
