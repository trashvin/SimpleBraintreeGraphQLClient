using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BraintreeQueryCreator.Model;
using BraintreeQueryCreator.Interface;
using System.Collections.Generic;
using System.Text;

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
        [DataTestMethod]
        [DataRow("arg1","argval1","False")]
        [DataRow("arg2",5,"True")]
        public void TestGQLArgument(string argName, object argValue, string isPrimitive)
        {
            bool isPrim = Boolean.Parse(isPrimitive);
            GQLArgument testArgs = new GQLArgument
            {
                Name = argName,
                Value = argValue,
                IsPrimitiveType = isPrim
            };

            var expected = isPrim ? $"{argName}:{argValue}" : $"{argName}:${argValue}";

            Assert.AreEqual(expected, testArgs.ToGQLString());
        }
        [DataTestMethod]
        [DataRow("field1","fieldVal1","True","","","")]
        [DataRow("field2",2,"True","field3","YES","True")]
        [DataRow("field4", "fieldVal4", "False", "", "", "")]
        [DataRow("field5", "MyObj", "False", "field6", "YourObj", "False")]
        [DataRow("field7", "fieldVal1", "False", "", "", "")]
        [DataRow("field8","HerObj", "False", "field3", "YES", "True")]
        public void TestGQLFieldGetArgumentString
        (
            string testName1,
            object testVal1,
            string isPrimitive1,
            string testName2,
            object testVal2,
            string isPrimitive2
        )
        {
            bool isPrim1 = bool.Parse(isPrimitive1);
            bool isPrim2 = String.IsNullOrEmpty(isPrimitive2) ? false : bool.Parse(isPrimitive2);
           
            GQLField testField = new GQLField
            {
                Field = new KeyValuePair<string, object>("Field1", "")
            };

            GQLArgument arg1 = new GQLArgument
            {
                Name = testName1,
                Value = testVal1,
                IsPrimitiveType = isPrim1
            };

            testField.Arguments = new List<IGQLQueryArgument>();
            testField.Arguments.Add(arg1);

            StringBuilder expected = new StringBuilder("(");

            if (isPrim1)
            {
                expected.Append($"{testName1}:{testVal1}");
            }
            else
            {
                expected.Append($"{testName1}:${testVal1}");
            }
            

            if (String.IsNullOrEmpty(testName2) != true)
            {
                GQLArgument arg2 = new GQLArgument
                {
                    Name = testName2,
                    Value = testVal2,
                    IsPrimitiveType = isPrim2
                };

                testField.Arguments.Add(arg2);

                if (isPrim2)
                {
                    expected.Append($",{testName2}:{testVal2}");
                }
                else
                {
                    expected.Append($",{testName2}:${testVal2}");
                }
                
            }
            expected.Append(")");

            Assert.AreEqual(expected.ToString(), testField.GetArgumentsString());
        }
        [DataTestMethod]
        [DataRow("field1", "fieldVal1", false, false)]
        [DataRow("field1", "fieldVal1", false, true)]
        [DataRow("field2", 2, false, false)]
        [DataRow("field2", 2, false, true)]
        [DataRow("field5", null, true, false)]
        [DataRow("field5", null, true, true)]
        public void TestGQLFieldToGQLString
        (
            string testName1,
            object testVal1,
            bool isNested,
            bool isRequest
        )
        {
            StringBuilder expected = new StringBuilder();

            GQLField subField = new GQLField
            {
                Field = new KeyValuePair<string, object>("innerField", (!isRequest)?"innerValue":null),
                IsNested = false,
                IsRequest = isRequest
            };

            if (isNested)
            {
                testVal1 = subField;
            }

            GQLField field = new GQLField
            {
                IsNested = isNested,
                IsRequest = isRequest
            };

            if (isNested)
            {
                field.Field = new KeyValuePair<string, object>(testName1, testVal1);  
            }
            else
            {
                if (!isRequest)
                {
                    field.Field = new KeyValuePair<string, object>(testName1, testVal1);
                }
                else
                {
                    field.Field = new KeyValuePair<string, object>(testName1, null);
                }
            }

            var result = field.ToGQLString();
            Console.WriteLine($"result = {result}");

            if (!isNested)
            {
                if (!isRequest)
                {
                    expected.Append($"{testName1}:{testVal1}");
                }
                else
                {
                    expected.Append(testName1);
                }
            }
            else
            {
                if (!isRequest)
                {
                    expected.Append($"{testName1}:");
                    expected.Append($"{{{subField.Field.Key}:{subField.Field.Value}}}");
                }
                else
                {
                    expected.Append(testName1);
                    expected.Append($"{{innerField}}");
                }
            }

            Console.WriteLine($"expected = {expected}");


            Assert.AreEqual(expected.ToString(), result);

        }
        //[TestMethod]
        //public void TestGQLQuerySimpleToGQLString()
        //{
        //    GQLField field = new GQLField
        //    {
        //        Field = new KeyValuePair<string, object>("ping", null)
        //    };
            
        //}



    }
}
