using System.Collections.Generic;

namespace GQLCCG.Infra
{
    public class GeneratorContext
    {
        public string Namespace { get; set; } = "GraphQlClient";

        public string MainClientFactoryClassName { get; set; } = "AppClientFactory";

        public IList<string> AdditionalClientUsing { get; set; } = new List<string>();

        public TypeNames TypeTrims { get; } = new TypeNames
        {
            Enum = "",
            InputObject = "Input|InputType",
            Interface = "Interface|InterfaceType",
            Object = "Object|ObjectType",
            Scalar = "",
            Union = "Union|UnionType",
        };

        public TypeNames Dto { get; } = new TypeNames
        {
            Enum = "{0}Dto",
            InputObject = "{0}Dto",
            Interface = "{0}Dto",
            Object = "{0}Dto",
            Scalar = "{0}Dto",
            Union = "{0}Dto",
        };

        public TypeNames Builder { get; } = new TypeNames
        {
            Enum = "{0}Builder",
            InputObject = "{0}Builder",
            Interface = "{0}Builder",
            Object = "{0}Builder",
            Scalar = "{0}Builder",
            Union = "{0}Builder",
        };



        public class TypeNames
        {
            public string Enum { get; set; }
            public string InputObject { get; set; }
            public string Interface { get; set; }
            public string Object { get; set; }
            public string Scalar { get; set; }
            public string Union { get; set; }
        }
    }
}