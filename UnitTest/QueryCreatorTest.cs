using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BraintreeQueryCreator.Model;

namespace ConnectorUnitTest
{
    [TestClass]
    public class QueryCreatorTest
    {
        [DataTestMethod]
        [DataRow("test_param","$test_param")]
        [DataRow("$$test","$$$test")]
        [DataRow("_test","$_test")]
        public void TestVariableModelGetParameterName(string test_name, string test_expected)
        {
            string name = test_name;
            string expected = test_expected;
            GraphQueryVariable testVariable = new GraphQueryVariable
            {
                Name = name,
                Value = "test",
                DataType = "Int!"
            };

            Assert.AreEqual(expected, testVariable.GetParameterName());
        }

        [DataTestMethod]
        [DataRow("var1", "value1", "String!")]
        [DataRow("var2", "3", "Int!")]
        public void TestVariableToVariableString(
            string test_name,
            string test_val,
            string test_type
        )
        {
            string expected = $"{test_name}:{test_val}";
            GraphQueryVariable variable = new GraphQueryVariable
            {
                Name = test_name,
                Value = test_val,
                DataType = test_type
            };

            Assert.AreEqual(expected, variable.ToVariableString());
        }

        [DataTestMethod]
        [DataRow("var1", "value1", "String!")]
        [DataRow("var2", "3", "Int!")]
        public void TestVariableToParameterString(
            string test_name,
            string test_val,
            string test_type
        )
        {
            GraphQueryVariable variable = new GraphQueryVariable
            {
                Name = test_name,
                Value = test_val,
                DataType = test_type
            };

            string expected = $"{variable.GetParameterName()}:{test_type}";

            Assert.AreEqual(expected, variable.ToParameterString());
        }
    }
}
