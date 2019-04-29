using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Models.Objects
{
    public class GraphQlInputValue : GraphQlEntityBase
    {
        public GraphQlTypeBase Type { get; internal set; }

        public string DefaultValue { get; }


        public GraphQlInputValue(
            string name,
            string description,
            GraphQlTypeBase type,
            string defaultValue)
            : base(name, description)
        {
            Type = type;
            DefaultValue = defaultValue;
        }
    }
}