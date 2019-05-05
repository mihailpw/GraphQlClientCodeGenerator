using GQLCCG.Infra;
using GQLCCG.Infra.Models.Types;

namespace Generator.DotNetCore.Helpers
{
    public class NameResolver
    {
        private readonly GeneratorContext _context;


        public NameResolver(GeneratorContext context)
        {
            _context = context;
        }


        public string ResolveDtoName(GraphQlTypeBase type)
        {
            switch (type)
            {
                case GraphQlListType listType:
                    return $"List<{ResolveDtoName(listType.OfType)}>";
                case GraphQlNonNullType nonNullType:
                    return ResolveDtoName(nonNullType.OfType);
                default:
                    return type.Name;
            }
        }

        public string ResolveGraphQlType(GraphQlTypeBase type)
        {
            switch (type)
            {
                case GraphQlListType listType:
                    return $"[{ResolveGraphQlType(listType.OfType)}]";
                case GraphQlNonNullType nonNullType:
                    return $"{ResolveGraphQlType(nonNullType.OfType)}!";
                default:
                    return type.Name;
            }
        }

        public string ResolveBuilderName(GraphQlTypeBase type)
        {
            return type.Name;
        }
    }
}