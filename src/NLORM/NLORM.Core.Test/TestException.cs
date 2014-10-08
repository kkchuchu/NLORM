using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLORM.Core.Test
{
    /// <summary>
    /// Summary description for TestException
    /// </summary>
    [TestClass]
    public class TestException
    {
        public TestException()
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

        [TestMethod]
        public void TestSupportDBTypeGen()
        {
            var cfd = new NLORM.Core.BasicDefinitions.ColumnFieldDefinition();
            cfd.FieldType = System.Data.DbType.Guid;
            var cfdDic = new Dictionary<string, BasicDefinitions.ColumnFieldDefinition>();
            cfdDic.Add("test1", cfd);
            var md = new NLORM.Core.BasicDefinitions.ModelDefinition("Test", cfdDic);
            var sqlGen = new BaseSqlGenerator();
            try
            {
                sqlGen.GenCreateTableSql(md);
            }
            catch (NLORM.Core.Exceptions.NLORMException nle)
            {
                Assert.AreEqual(nle.ErrorCode, "SG");
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}
