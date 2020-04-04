using BraintreeQueryCreator.Interface;
using System;
using System.Collections.Generic;

namespace BraintreeQueryCreator.Model
{
    public class GraphQuery : IGraphQuery
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public string AddParameter()
        {
            throw new System.NotImplementedException();
        }

        public void AddSubQuery(IGraphQuery subQuery)
        {
            throw new System.NotImplementedException();
        }

        public string ToGraphQueryString()
        {
            throw new System.NotImplementedException();
        }
    }

    public class GraphQueryVariable : IGraphQueryVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }

        public string GetParameterName() => $"${Name}";

        public string ToParameterString()
        {
            try
            {
                return $"{GetParameterName()}:{DataType}";
            }
            catch(Exception ex)
            {
                return String.Empty;
            }
        }

        public string ToVariableString()
        {
            try
            {
                return $"{Name}:{Value}";
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
    }

    public class GraphQueryParameter : IGraphQueryParameter
    {
        public string Name { get; set; }
        public string MappedVariable { get; set; }
    }


}