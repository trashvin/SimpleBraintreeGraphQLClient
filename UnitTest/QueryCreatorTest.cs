using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BraintreeQueryCreator.Model;
using System.Collections.Generic;

namespace ConnectorUnitTest
{
    [TestClass]
    public class QueryCreatorTest
    {
        [DataTestMethod]
        [DataRow("test_param","$test_param")]
        [DataRow("$$test","$$$test")]
        [DataRow("_test","$_test")]
        public void TestGQLVariableGetVariableName(string test_name, string test_expected)
        {

            GQLVariable variable = new GQLVariable
            {
                VariableField = new GQLField
                {
                    Field = new KeyValuePair<string, object>(test_name, "Value")        
                },
                DataType = "Int"
            };

            Assert.AreEqual(test_expected, variable.GetParameterName());
        }

        [DataTestMethod]
        [DataRow("var1", "value1", "String")]
        [DataRow("var2", 3, "Int")]
        public void TestGQLVariableToGQLString(
            string test_name,
            object test_val,
            string test_type
        )
        {
            string expected = $"{test_name}:{test_val}";
            GQLVariable variable = new GQLVariable
            {
                VariableField = new GQLField
                {
                    Field = new KeyValuePair<string, object>(test_name, test_val)
                },
                DataType = test_type
            };

            Assert.AreEqual(expected, variable.ToGQLString());
        }

        [DataTestMethod]
        [DataRow("var1", "value1", "String")]
        [DataRow("var2", 4.00, "Int")]
        public void TestGQLVariableToParameterString(
            string test_name,
            object test_val,
            string test_type
        )
        {
            GQLVariable variable = new GQLVariable
            {
                VariableField = new GQLField
                {
                    Field = new KeyValuePair<string, object>(test_name, test_val)
                },
                DataType = test_type
            };

            string expected = $"{variable.GetParameterName()}:{test_type}!";

            Assert.AreEqual(expected, variable.ToParameterString());
        }
    
        
    
    }
}
