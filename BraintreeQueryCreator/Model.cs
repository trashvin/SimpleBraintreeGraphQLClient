using BraintreeQueryCreator.Interface;
using System;
using System.Collections.Generic;

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
        public List<IGqlVariable> Variable { get; set; }

        public string ToGQLString()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class GQLField : IGQLField
    {
        public KeyValuePair<string, object> Field { get; set; }
        public List<IGQLQueryArgument> Arguments { get; set; }

        public string ToGQLString()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return base.ToString();
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
                $"${Name}:{Value}";
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