namespace GQLCCG.Infra.Models
{
    public class GraphQlEntityBase
    {
        public string Name { get; }

        public string Description { get; }


        public GraphQlEntityBase(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}