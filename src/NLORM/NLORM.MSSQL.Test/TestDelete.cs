using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.MSSQL.Test
{
    [TestClass]
    public class TestDelete
    {
        public class TestClassOne
        {
            public string Id { get; set; }

            public int income { get; set; }
        }

        private string connectionString = NLORMSSQLDbTest.coonectionstring;

        [TestInitialize()]
        public void TestInitialize()
        {
            try
            {
                var db = new NLORMMSSQLDb(connectionString);
                new NLORMMSSQLDb(connectionString);
                this.createtable(db);
                this.insertdata(db);
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
        private INLORMDb createtable()
        {
            var db = new NLORMMSSQLDb(connectionString);
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
        public void TestDeleteOneRecord()
        {
            var db = new NLORMMSSQLDb(connectionString);
            int deletedcount = db.FliterBy(FliterType.EQUAL_AND, new { Id = "sssss" }).Delete<TestClassOne>();
            Assert.AreEqual(deletedcount, 1);
        }

        [TestMethod]
        public void TestDeleteTwoRecord()
        {
            var db = new NLORMMSSQLDb(connectionString);
            db.FliterBy(FliterType.EQUAL_AND, new { income = 901234 }).Delete<TestClassOne>();
            var items = db.Query<TestClassOne>();
            Assert.AreEqual(3, items.Count());
        }

        [TestMethod]
        public void TestDeleteAllRecords()
        {
            var db = new NLORMMSSQLDb(connectionString);
            int totalcount = db.Query<TestClassOne>().Count();
            int dc = db.Delete<TestClassOne>();

            var items = db.Query<TestClassOne>();
            Assert.AreEqual(dc, totalcount);
        }
    }
}
