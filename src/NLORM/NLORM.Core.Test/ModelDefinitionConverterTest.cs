using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLORM.Core.Attributes;

namespace NLORM.Core.Test
{
    [TableName("TestTableA")]
    class TestClassA
    {

    }

    class TestClassB
    {
    }

    /// <summary>
    /// Summary description for ModelDefinitionConverterTest
    /// </summary>
    [TestClass]
    public class ModelDefinitionConverterTest
    {
        public ModelDefinitionConverterTest()
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
        public void TestGetTableName()
        {
            var mdc = ModelDefinitionConverter.Instance;
            var md = mdc.ConverClassToModelDefinition<TestClassA>();
            string tableName = md.TableName;
            Assert.AreEqual("TestTableA", tableName);
        }

        [TestMethod]
        public void TestGetTableNameWithoutAttr()
        {
            var mdc = ModelDefinitionConverter.Instance;
            var md = mdc.ConverClassToModelDefinition<TestClassB>();
            string tableName = md.TableName;
            Assert.AreEqual("TestClassB", tableName);
        }
    }
}
