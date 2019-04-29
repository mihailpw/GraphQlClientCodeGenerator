using System;
using System.Collections.Generic;
using System.Linq;
using GQLCCG.Infra.Models;
using GQLCCG.Infra.Models.Objects;
using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Processor.SchemaReaders
{
    internal class ConvertGraphQlJsonDtoToModelCommand
    {
        public GraphQlSchema Execute(GraphQlJsonDto.Schema dto)
        {
            var types = ConvertToTypesWithRefs(dto.Types).ToList();
            PopulateTypesWithRefs(types);

            var queryType = SelectObjectType(dto.QueryType, types);
            var mutationType = SelectObjectType(dto.MutationType, types);
            var subscriptionType = SelectObjectType(dto.SubscriptionType, types);

            var objects = types
                .Where(t => t.GetType() != typeof(GraphQlScalarType)
                            && t.Name != queryType?.Name
                            && t.Name != mutationType?.Name
                            && t.Name != subscriptionType?.Name)
                .ToList();

            return new GraphQlSchema(queryType, mutationType, subscriptionType, objects);
        }


        private static IEnumerable<GraphQlTypeBase> ConvertToTypesWithRefs(IEnumerable<GraphQlJsonDto.FullType> types)
        {
            GraphQlTypeBase ConvertToTypeRef(GraphQlJsonDto.ShortType shortType)
            {
                switch (shortType.Kind)
                {
                    case "LIST":
                        return new GraphQlListType(
                            shortType.Name,
                            shortType.Description,
                            ConvertToTypeRef(shortType.OfType));
                    case "NON_NULL":
                        return new GraphQlNonNullType(
                            shortType.Name,
                            shortType.Description,
                            ConvertToTypeRef(shortType.OfType));
                    default:
                        return new TypeRef(shortType.Name);
                }

            }
            IReadOnlyList<GraphQlTypeBase> ConvertToTypeRefs(IEnumerable<GraphQlJsonDto.ShortType> shortTypes)
            {
                if (shortTypes == null)
                {
                    return Array.Empty<GraphQlObjectType>();
                }

                return shortTypes
                    .Select(ConvertToTypeRef)
                    .ToList();
            }
            IReadOnlyList<GraphQlInputValue> ConvertToInputValues(IEnumerable<GraphQlJsonDto.InputValue> inputValues)
            {
                if (inputValues == null)
                {
                    return Array.Empty<GraphQlInputValue>();
                }

                return inputValues
                    .Select(f => new GraphQlInputValue(
                        f.Name,
                        f.Description,
                        ConvertToTypeRef(f.Type),
                        f.DefaultValue))
                    .ToList();
            }
            IReadOnlyList<GraphQlField> ConvertToFields(IEnumerable<GraphQlJsonDto.Field> fields)
            {
                if (fields == null)
                {
                    return Array.Empty<GraphQlField>();
                }

                return fields
                    .Select(f => new GraphQlField(
                        f.Name,
                        f.Description,
                        f.IsDeprecated,
                        f.DeprecationReason,
                        ConvertToTypeRef(f.Type),
                        ConvertToInputValues(f.Args)))
                    .ToList();
            }
            IReadOnlyList<GraphQlEnumValue> ConvertToEnumValues(IEnumerable<GraphQlJsonDto.EnumValue> fields)
            {
                if (fields == null)
                {
                    return Array.Empty<GraphQlEnumValue>();
                }

                return fields
                    .Select(f => new GraphQlEnumValue(
                        f.Name,
                        f.Description,
                        f.IsDeprecated,
                        f.DeprecationReason))
                    .ToList();
            }

            foreach (var type in types)
            {
                if (type.Name.StartsWith("__"))
                {
                    continue;
                }

                switch (type.Kind)
                {
                    case "SCALAR":
                        var scalarType = (ScalarTypes) Enum.Parse(typeof(ScalarTypes), type.Name, true);
                        yield return new GraphQlScalarType(
                            name: type.Name,
                            description: type.Description,
                            type: scalarType);
                        break;
                    case "OBJECT":
                        yield return new GraphQlObjectType(
                            name: type.Name,
                            description: type.Description,
                            fields: ConvertToFields(type.Fields),
                            possibleTypes: ConvertToTypeRefs(type.PossibleTypes));
                        break;
                    case "INTERFACE":
                        yield return new GraphQlInterfaceType(
                            name: type.Name,
                            description: type.Description,
                            fields: ConvertToFields(type.Fields),
                            possibleTypes: ConvertToTypeRefs(type.PossibleTypes));
                        break;
                    case "UNION":
                        yield return new GraphQlUnionType(
                            name: type.Name,
                            description: type.Description,
                            possibleTypes: ConvertToTypeRefs(type.PossibleTypes));
                        break;
                    case "ENUM":
                        yield return new GraphQlEnumType(
                            name: type.Name,
                            description: type.Description,
                            enumValues: ConvertToEnumValues(type.EnumValues));
                        break;
                    case "INPUT_OBJECT":
                        yield return new GraphQlInputObjectType(
                            name: type.Name,
                            description: type.Description,
                            inputFields: ConvertToInputValues(type.InputFields));
                        break;
                    case "LIST":
                    case "NON_NULL":
                        throw new InvalidOperationException($"Kind '{type.Kind}' is not allowed here.");
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type.Kind), type.Kind, null);
                }
            }
        }

        private static void PopulateTypesWithRefs(List<GraphQlTypeBase> types)
        {
            IReadOnlyList<GraphQlTypeBase> LockupList(IEnumerable<GraphQlTypeBase> typeBases)
            {
                return typeBases.Select(Lockup).ToList();
            }
            GraphQlTypeBase Lockup(GraphQlTypeBase typeBase)
            {
                switch (typeBase)
                {
                    case GraphQlListType listType:
                        listType.OfType = Lockup(listType.OfType);
                        return listType;
                    case GraphQlNonNullType nonNullType:
                        nonNullType.OfType = Lockup(nonNullType.OfType);
                        return nonNullType;
                    case TypeRef typeRef:
                        var lockupType = types.SingleOrDefault(t => t.Name == typeRef.Name);
                        switch (lockupType)
                        {
                            case null:
                                throw new InvalidOperationException($"Ref '{typeRef.Name}' not found.");
                            case TypeRef _:
                                throw new InvalidOperationException($"Something gone wrong. {nameof(TypeRef)} could not be here.");
                            default:
                                return lockupType;
                        }
                    default:
                        return typeBase;
                }
            }

            void PopulateInputValues(IEnumerable<GraphQlInputValue> inputValues)
            {
                foreach (var inputValue in inputValues)
                {
                    inputValue.Type = Lockup(inputValue.Type);
                }
            }
            void PopulateFields(IEnumerable<GraphQlField> fields)
            {
                foreach (var field in fields)
                {
                    field.Type = Lockup(field.Type);
                    PopulateInputValues(field.Args);
                }
            }

            foreach (var type in types)
            {
                switch (type)
                {
                    case GraphQlInputObjectType inputObjectType:
                        PopulateInputValues(inputObjectType.InputFields);
                        break;
                    case GraphQlInterfaceType interfaceType:
                        PopulateFields(interfaceType.Fields);
                        interfaceType.PossibleTypes = LockupList(interfaceType.PossibleTypes);
                        break;
                    case GraphQlObjectType objectType:
                        PopulateFields(objectType.Fields);
                        objectType.PossibleTypes = LockupList(objectType.PossibleTypes);
                        break;
                    case GraphQlUnionType unionType:
                        unionType.PossibleTypes = LockupList(unionType.PossibleTypes);
                        break;
                    case GraphQlListType _:
                    case GraphQlNonNullType _:
                        throw new InvalidOperationException($"Type '{type.GetType().Name}' is not allowed here.");
                }
            }
        }

        private static GraphQlObjectType SelectObjectType(GraphQlJsonDto.ShortType type, List<GraphQlTypeBase> types)
        {
            if (type == null)
            {
                return null;
            }

            return (GraphQlObjectType) types.Single(t => t.Name == type.Name);
        }



        private class TypeRef : GraphQlTypeBase
        {
            public TypeRef(string name)
                : base(name, null)
            {
            }
        }
    }
}