using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLORM.MSSQL.Test
{
    public class TestDelete
    {
        class TestClassOne
        {
            public string Id { get; set; }

            public int income { get; set; }
        }

        private string constr = NLORMSSQLDbTest.coonectionstring;

        [TestInitialize()]
        public void TestInitialize()
        {
            INLORMDb msdb = null;
            try
            {
                msdb = new NLORMMSSQLDb(constr);
                msdb.DropTable<TestClassOne>();
                var db = this.createtable();
                this.insertdata(db);
            }
            catch (Exception)
            {
            }
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            INLORMDb mssqlDb = null;
            try
            {
                mssqlDb = new NLORMMSSQLDb(constr);
                mssqlDb.DropTable<TestClassOne>();
            }
            catch (Exception)
            {
            }
        }
        private INLORMDb createtable()
        {
            var db = new NLORMMSSQLDb(constr);
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
            var db = new NLORMMSSQLDb(constr);
            int deletedcount = db.FliterBy(FliterType.EQUAL_AND, new { Id = "sssss" }).Delete<TestClassOne>();
            Assert.AreEqual(deletedcount, 1);
        }

        [TestMethod]
        public void TestDeleteOneRecord()
        {
            var db = new NLORMMSSQLDb(constr);
            int i = db.FliterBy(FliterType.EQUAL_AND, new { Id = "sssss" }).Delete<TestClassOne>();
            var items = db.FliterBy(FliterType.EQUAL_AND, new { Id = "sssss" }).Query<TestClassOne>();
            Assert.AreEqual(0, items.Count());
        }

        [TestMethod]
        public void TestDeleteTwoRecord()
        {
            var db = new NLORMMSSQLDb(constr);
            db.FliterBy(FliterType.EQUAL_AND, new { income = 901234 }).Delete<TestClassOne>();
            var items = db.Query<TestClassOne>();
            Assert.AreEqual(3, items.Count());
        }

        [TestMethod]
        public void TestDeleteAllRecords()
        {
            var db = new NLORMMSSQLDb(constr);
            int totalcount = db.Query<TestClassOne>().Count();
            int dc = db.Delete<TestClassOne>();

            var items = db.Query<TestClassOne>();
            Assert.AreEqual(dc, totalcount);
        }
    }
}
