using BraintreeQueryCreator.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BraintreeQueryCreator.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class GQLQuery : IGQLQuery
    {
        public string Operation { get; set; }
        public string OperationName { get; set; }
        public IGQLField Field { get; set; }
        public List<IGqlVariable> Variables { get; set; }
        //Todo: Add test
        public string ToGQLString()
        {
            StringBuilder query = new StringBuilder(Operation);
            int levelCounter = 0;

            if (String.IsNullOrEmpty(OperationName) == false)
            {
                query.Append(OperationName);
            }

            query.Append(Field.ToGQLString());
            
            levelCounter++;

            return query.ToString();

        }
        public override string ToString()
        {
            return $"[DQLQuery = operation:{Operation} ; OperationName:{OperationName}; Field:[{Field.ToGQLString()}]; Variables = {Variables.Count}]";
        }
    }

    public class GQLField : IGQLField
    {
        public KeyValuePair<string, object> Field { get; set; }
        public List<IGQLQueryArgument> Arguments { get; set; }
        public bool IsNested { get; set; }
        public bool IsRequest { get; set; }
        //Todo: Add arguments
        public string ToGQLString()
        {
            StringBuilder fieldString = new StringBuilder();

            fieldString.Append(Field.Key);
            
            if (!IsNested)
            {
                if (!IsRequest)
                {
                    fieldString.Append($":{Field.Value}");
                }
                else
                {
                    if (Arguments != null)
                    {
                        fieldString.Append("(");
                        var withElement = false;
                        foreach(IGQLQueryArgument arg in Arguments)
                        {
                            if (withElement)
                            {
                                fieldString.Append($",{arg.ToGQLString()}");
                            }
                            else
                            {
                                withElement = true;
                                fieldString.Append($"{arg.ToGQLString()}");
                            }
                            
                        }
                        fieldString.Append(")");

                    }
                }
            }
            else
            {        
                var expandedString = ((IGQLField)Field.Value).ToGQLString();
                if(IsRequest)
                {
                    if (Arguments != null)
                    {
                        fieldString.Append("(");
                        var withElement = false;
                        foreach (IGQLQueryArgument arg in Arguments)
                        {
                            if (withElement)
                            {
                                fieldString.Append($",{arg.ToGQLString()}");
                            }
                            else
                            {
                                withElement = true;
                                fieldString.Append($"{arg.ToGQLString()}");
                            }

                        }
                        fieldString.Append(")");

                    }
                    fieldString.Append($"{{{expandedString}}}");
                }
                else
                {
                    fieldString.Append($":{{{expandedString}}}");
                }
                 
            }

            return fieldString.ToString();
        }
        public override string ToString()
        {
            return $"GQLField = Name:{Field.Key};Value:{Field.Value};IsNested:{IsNested}";
        }
        public string GetArgumentsString()
        {
            StringBuilder argString = new StringBuilder();

            if (Arguments != null)
            {
                argString.Append("(");
                foreach(IGQLQueryArgument arg in Arguments)
                {
                    argString.Append($"{arg.ToGQLString()},");
                }
                argString.Remove(argString.Length - 1, 1);
                argString.Append(")");

                return argString.ToString();
            }

            return null;
        }
    }

    public class GQLArgument : IGQLQueryArgument
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public bool IsPrimitiveType { get; set; }

        public string ToGQLString()
        {
            return IsPrimitiveType ?
                $"{Name}:{Value}" :
                $"{Name}:${Value}";
        }
        public override string ToString()
        {
            return $"GQLArgument = Name:{Name};Value:{Value};IsPrimitiveType:{IsPrimitiveType}";
        }
    }

    public class GQLVariable : IGqlVariable
    {
        public IGQLField VariableField { get; set; }
        public string DataType { get; set; }
        public List<IGqlVariable> Variables { get; set; }

        public string GetParameterName()
        {
            return $"${VariableField.Field.Key}";
        }

        public string ToGQLString()
        {
            return $"{VariableField.Field.Key}:{VariableField.Field.Value}";
        }

        public string ToParameterString()
        {
            return $"{GetParameterName()}:{DataType}!";
        }
        public override string ToString()
        {
            return $"GQLVariable = Name:{VariableField.Field.Key};" +
                $"Value:{VariableField.Field.Value};DataType:{DataType}";
        }

    }
}