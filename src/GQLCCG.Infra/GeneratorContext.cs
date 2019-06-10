using System.Collections.Generic;
using GQLCCG.Infra.Utils;

namespace GQLCCG.Infra
{
    public class GeneratorContext
    {
        public string Namespace { get; set; } = "GraphQlClient";

        public string MainClientFactoryClassName { get; set; } = "AppClientFactory";

        public IList<string> AdditionalClientUsing { get; set; } = new List<string>();

        public bool GenerateDocs { get; set; } = false;

        public bool GenerateInputObjectConstructor { get; set; } = false;

        public TypeNames Names { get; } = new TypeNames
        {
            DtoEnum = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "{0}Enum" },
            DtoInputObject = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "{0}Dto" },
            DtoInterface = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "{0}Dto" },
            DtoObject = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "{0}Dto" },
            DtoUnion = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "{0}Dto" },
            BuilderInterface = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "{0}Builder" },
            BuilderObject = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "{0}Builder" },
            BuilderUnion = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "{0}Builder" },
            ConstructionOnType = new TypeNames.NameEntry { RemoveRegex = "Type", BuildFormat = "On{0}" },
        };
    }
}