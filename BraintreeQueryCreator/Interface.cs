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
        List<IGqlVariable> Variables { get; set; }
        string ToGQLString();
        string ToString();
    }

    /// <summary>
    /// { name:value }
    /// note: default for value is String.Empty 
    /// </summary>
    public interface IGQLField
    {
        /// <summary>
        /// Format: name : value
        /// </summary>
        KeyValuePair<string, object> Field { get; set; }
        List<IGQLQueryArgument> Arguments { get; set; }
        string ToGQLString();
        bool IsNested { get; set; }
        bool IsRequest { get; set; }
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
        /// <summary>
        /// Returns the parameter name : data type
        /// </summary>
        /// <returns></returns>
        string ToParameterString();
        /// <summary>
        /// Gets the parameter name : $name
        /// </summary>
        /// <returns></returns>
        string GetParameterName();
        List<IGqlVariable> Variables { get; set; }
        string ToGQLString();
       
    }

}