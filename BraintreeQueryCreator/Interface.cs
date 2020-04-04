using System.Collections.Generic;

namespace BraintreeQueryCreator.Interface
{
    public interface IGraphQuery
    {
        string Type { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        string AddParameter();
        void AddSubQuery(IGraphQuery subQuery);
        string ToGraphQueryString();
    }

    public interface IGraphQueryParameter
    {
        string Name { get; set; }
        string MappedVariable { get; set; }
    }

    public interface IGraphQueryVariable
    {
        string Name { get; set; }
        string Value { get; set; }
        string DataType { get; set; }
        string ToVariableString();
        string ToParameterString();
        string GetParameterName();
    }

}