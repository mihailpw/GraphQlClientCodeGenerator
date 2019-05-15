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


        public string ResolveDtoName(GraphQlTypeBase type, bool withNullable)
        {
            string Resolve(GraphQlTypeBase typeInternal, bool withNullableInternal, int level)
            {
                var nullableSign = withNullableInternal && level == 0
                    ? "?"
                    : string.Empty;

                switch (typeInternal)
                {
                    case GraphQlListType listType:
                        return $"List<{Resolve(listType.OfType, withNullableInternal, level + 1)}>";
                    case GraphQlNonNullType nonNullType:
                        return Resolve(nonNullType.OfType, withNullableInternal, level + 1);
                    case GraphQlEnumType enumType:
                        return $"{enumType.Name}{nullableSign}";
                    case GraphQlScalarType scalarType:
                        switch (scalarType.Type)
                        {
                            case ScalarTypes.String:
                                return "string";
                            case ScalarTypes.Boolean:
                                return $"bool{nullableSign}";
                            case ScalarTypes.Float:
                                return $"float{nullableSign}";
                            case ScalarTypes.Int:
                                return $"int{nullableSign}";
                            case ScalarTypes.Id:
                                return "string";
                            case ScalarTypes.Date:
                                return $"DateTime{nullableSign}";
                            case ScalarTypes.DateTime:
                                return $"DateTime{nullableSign}";
                            case ScalarTypes.DateTimeOffset:
                                return $"DateTimeOffset{nullableSign}";
                            case ScalarTypes.Seconds:
                                return $"long{nullableSign}";
                            case ScalarTypes.Milliseconds:
                                return $"long{nullableSign}";
                            case ScalarTypes.Decimal:
                                return $"decimal{nullableSign}";
                            default:
                                throw new ArgumentOutOfRangeException(nameof(scalarType.Type), scalarType.Type, null);
                        }
                    default:
                        return $"{typeInternal.Name}Dto";
                }
            }

            return Resolve(type, withNullable, 0);
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
        public string ResolveOnTypeMethodName(GraphQlTypeBase type)
        {
            return $"On{type.Name.ToPascal()}";
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