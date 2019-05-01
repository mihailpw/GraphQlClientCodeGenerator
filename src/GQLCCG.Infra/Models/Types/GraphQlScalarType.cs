namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlScalarType : GraphQlTypeBase
    {
        public ScalarTypes Type { get; set; }
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