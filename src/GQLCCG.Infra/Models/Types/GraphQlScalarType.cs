namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlScalarType : GraphQlTypeBase
    {
        public ScalarTypes Type { get; }


        public GraphQlScalarType(
            string name,
            string description,
            ScalarTypes type)
            : base(name, description)
        {
            Type = type;
        }
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