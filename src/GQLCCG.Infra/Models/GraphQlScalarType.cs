using System;

namespace GQLCCG.Infra.Models
{
    public class GraphQlScalarType : GraphQlType
    {
        private ScalarTypes? _type;


        public ScalarTypes Type => _type ?? (_type = (ScalarTypes) Enum.Parse(typeof(ScalarTypes), Name, true)).Value;
    }



    public enum ScalarTypes
    {
        String,
        Boolean,
        Float,
        Int,
        Id,
        Date,
        DateTime,
        DateTimeOffset,
        Seconds,
        Milliseconds,
        Decimal,
    }
}