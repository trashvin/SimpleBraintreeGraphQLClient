using System.Collections.Generic;

namespace BraintreeQueryCreator.Interface
{
    /// <summary>
    /// operation [operationName]{
    ///    field {
    ///      field
    ///    }
    /// }
    /// </summary>
    public interface IGQLQuery
    {
        string Operation { get; set; }
        string OperationName { get; set; }
        IGQLField Field { get; set; }
        List<IGqlVariable> Variable { get; set; }
        string ToGQLString();
        string ToString();
    }

    /// <summary>
    /// { name:value }
    /// note: default for value is String.Empty 
    /// </summary>
    public interface IGQLField
    {
        KeyValuePair<string, object> Field { get; set; }
        List<IGQLQueryArgument> Arguments { get; set; }
        string ToGQLString();

    }

    public interface IGQLQueryArgument
    {
        string Name { get; set; }
        object Value { get; set; }
        // List<IGqlVariable> Variable { get; set; }
        string ToGQLString();
        bool IsPrimitiveType { get; set; }
       
    }

    /// <summary>
    /// variables {
    ///    field
    /// }
    /// </summary>
    public interface IGqlVariable
    {
        IGQLField VariableField { get; set; }
        string DataType { get; set; }
        //string ToVariableString();
        string ToParameterString();
        string GetParameterName();
        List<IGqlVariable> Variables { get; set; }
        string ToGQLString();
       
    }

}