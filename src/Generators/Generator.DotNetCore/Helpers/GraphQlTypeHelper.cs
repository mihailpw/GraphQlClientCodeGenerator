using System;
using System.ComponentModel;
using GQLCCG.Infra;
using GQLCCG.Infra.Models.Objects;
using GQLCCG.Infra.Models.Types;
using GQLCCG.Infra.Utils;

namespace Generator.DotNetCore.Helpers
{
    public class GraphQlTypeHelper
    {
        private readonly GeneratorContext _context;


        public GraphQlTypeHelper(GeneratorContext context)
        {
            _context = context;
        }


        [AutoWire]
        public string ResolveDtoName(GraphQlTypeBase type)
        {
            switch (type)
            {
                case GraphQlListType listType:
                    return $"List<{ResolveDtoName(listType.OfType)}>";
                case GraphQlNonNullType nonNullType:
                    return ResolveDtoName(nonNullType.OfType);
                case GraphQlScalarType scalarType:
                    switch (scalarType.Type)
                    {
                        case ScalarTypes.String:
                            return nameof(String);
                        case ScalarTypes.Boolean:
                            return nameof(Boolean);
                        case ScalarTypes.Float:
                            return nameof(Single);
                        case ScalarTypes.Int:
                            return nameof(Int32);
                        case ScalarTypes.Id:
                            return nameof(String);
                        case ScalarTypes.Date:
                            return nameof(DateTime);
                        case ScalarTypes.DateTime:
                            return nameof(DateTime);
                        case ScalarTypes.DateTimeOffset:
                            return nameof(DateTimeOffset);
                        case ScalarTypes.Seconds:
                            return nameof(Int64);
                        case ScalarTypes.Milliseconds:
                            return nameof(Int64);
                        case ScalarTypes.Decimal:
                            return nameof(Decimal);
                        default:
                            throw new ArgumentOutOfRangeException(nameof(scalarType.Type), scalarType.Type, null);
                    }
                default:
                    return $"{type.Name}Dto";
            }
        }

        [AutoWire]
        public string ResolveGraphQlTypeName(GraphQlTypeBase type)
        {
            switch (type)
            {
                case GraphQlListType listType:
                    return $"[{ResolveGraphQlTypeName(listType.OfType)}]";
                case GraphQlNonNullType nonNullType:
                    return $"{ResolveGraphQlTypeName(nonNullType.OfType)}!";
                default:
                    return type.Name;
            }
        }

        [AutoWire]
        public string ResolveBuilderName(GraphQlTypeBase type)
        {
            type = type.GetRealType();

            switch (type)
            {
                case GraphQlInterfaceType _:
                case GraphQlObjectType _:
                case GraphQlUnionType _:
                    return $"{type.Name}Builder";
                case GraphQlScalarType scalarType:
                    return ResolveDtoName(scalarType);
                default:
                    throw new InvalidOperationException("Can not be a builder.");
            }
        }

        [AutoWire]
        public string ResolveDefaultValue(GraphQlInputValue inputValue, string format)
        {
            return inputValue.Type is GraphQlNonNullType
                ? string.Empty
                : string.Format(format, inputValue.DefaultValue);
        }

        public bool IfKind(GraphQlTypeBase type, string[] kindValues)
        {
            type = type.GetRealType();

            foreach (var kindValue in kindValues)
            {
                if (!Enum.TryParse<GraphQlKind>(kindValue, true, out var kind))
                {
                    throw new InvalidEnumArgumentException();
                }

                if (type.Kind == kind)
                {
                    return true;
                }
            }

            return false;
        }



        [AttributeUsage(AttributeTargets.Method)]
        public class AutoWire : Attribute { }
    }
}