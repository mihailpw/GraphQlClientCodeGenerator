using System;
using System.Collections.Generic;
using System.Linq;
using GQLCCG.Infra.Models;
using GQLCCG.Infra.Models.Objects;
using GQLCCG.Infra.Models.Types;
using GQLCCG.Processor.Core;

namespace GQLCCG.Processor.SchemaReaders
{
    internal class ConvertGraphQlJsonDtoToModelCommand
    {
        public GraphQlSchema Execute(GraphQlJsonDto.Schema dto)
        {
            var types = ConvertToTypesWithRefs(dto.Types).ToList();
            PopulateTypesWithRefs(types);
            PostProcessTypes(types);

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


        private static IEnumerable<GraphQlTypeBase> ConvertToTypesWithRefs(IEnumerable<GraphQlJsonDto.Type> types)
        {
            GraphQlTypeBase ConvertToTypeRef(GraphQlJsonDto.TypeRef shortType)
            {
                switch (shortType.Kind)
                {
                    case "LIST":
                        return new GraphQlListType
                        {
                            Name = shortType.Name,
                            Description = shortType.Description,
                            OfType = ConvertToTypeRef(shortType.OfType),
                        };
                    case "NON_NULL":
                        return new GraphQlNonNullType
                        {
                            Name = shortType.Name,
                            Description = shortType.Description,
                            OfType = ConvertToTypeRef(shortType.OfType),
                        };
                    default:
                        return new TypeRef(shortType.Name);
                }

            }
            IList<GraphQlTypeBase> ConvertToTypeRefs(IEnumerable<GraphQlJsonDto.TypeRef> shortTypes)
            {
                if (shortTypes == null)
                {
                    return Array.Empty<GraphQlTypeBase>();
                }

                return shortTypes
                    .Select(ConvertToTypeRef)
                    .ToList();
            }
            IList<GraphQlInputValue> ConvertToInputValues(IEnumerable<GraphQlJsonDto.InputValue> inputValues)
            {
                if (inputValues == null)
                {
                    return Array.Empty<GraphQlInputValue>();
                }

                return inputValues
                    .Select(f => new GraphQlInputValue
                    {
                        Name = f.Name,
                        Description = f.Description,
                        Type = ConvertToTypeRef(f.Type),
                        DefaultValue = f.DefaultValue,
                    })
                    .ToList();
            }
            IList<GraphQlField> ConvertToFields(IEnumerable<GraphQlJsonDto.Field> fields)
            {
                if (fields == null)
                {
                    return Array.Empty<GraphQlField>();
                }

                return fields
                    .Select(f => new GraphQlField
                    {
                        Name = f.Name,
                        Description = f.Description,
                        IsDeprecated = f.IsDeprecated,
                        DeprecationReason = f.DeprecationReason,
                        Type = ConvertToTypeRef(f.Type),
                        Args = ConvertToInputValues(f.Args),
                    })
                    .ToList();
            }
            IList<GraphQlEnumValue> ConvertToEnumValues(IEnumerable<GraphQlJsonDto.EnumValue> fields)
            {
                if (fields == null)
                {
                    return Array.Empty<GraphQlEnumValue>();
                }

                return fields
                    .Select(f => new GraphQlEnumValue
                    {
                        Name = f.Name,
                        Description = f.Description,
                        IsDeprecated = f.IsDeprecated,
                        DeprecationReason = f.DeprecationReason,
                    })
                    .ToList();
            }

            foreach (var type in types)
            {
                if (CheckIfSystemType(type))
                {
                    continue;
                }

                switch (type.Kind)
                {
                    case "SCALAR":
                        var scalarType = (ScalarTypes) Enum.Parse(typeof(ScalarTypes), type.Name, true);
                        yield return new GraphQlScalarType
                        {
                            Name = type.Name,
                            Description = type.Description,
                            Type = scalarType,
                        };
                        break;
                    case "OBJECT":
                        yield return new GraphQlObjectType
                        {
                            Name = type.Name,
                            Description = type.Description,
                            Fields = ConvertToFields(type.Fields),
                            PossibleTypes = ConvertToTypeRefs(type.PossibleTypes),
                        };
                        break;
                    case "INTERFACE":
                        yield return new GraphQlInterfaceType
                        {
                            Name = type.Name,
                            Description = type.Description,
                            Fields = ConvertToFields(type.Fields),
                            PossibleTypes = ConvertToTypeRefs(type.PossibleTypes),
                        };
                        break;
                    case "UNION":
                        yield return new GraphQlUnionType
                        {
                            Name = type.Name,
                            Description = type.Description,
                            PossibleTypes = ConvertToTypeRefs(type.PossibleTypes),
                        };
                        break;
                    case "ENUM":
                        yield return new GraphQlEnumType
                        {
                            Name = type.Name,
                            Description = type.Description,
                            EnumValues = ConvertToEnumValues(type.EnumValues),
                        };
                        break;
                    case "INPUT_OBJECT":
                        yield return new GraphQlInputObjectType
                        {
                            Name = type.Name,
                            Description = type.Description,
                            InputFields = ConvertToInputValues(type.InputFields),
                        };
                        break;
                    case "LIST":
                    case "NON_NULL":
                        throw new InvalidOperationException($"Kind '{type.Kind}' is not allowed here.");
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type.Kind), type.Kind, null);
                }
            }
        }

        private static void PopulateTypesWithRefs(IReadOnlyCollection<GraphQlTypeBase> types)
        {
            IList<GraphQlTypeBase> LockupList(IEnumerable<GraphQlTypeBase> typeBases)
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

        private static void PostProcessTypes(IEnumerable<GraphQlTypeBase> types)
        {
            foreach (var type in types)
            {
                var fieldsHolder = type as IGraphQlFieldsHolder;

                if (fieldsHolder != null)
                {
                    foreach (var field in fieldsHolder.Fields)
                    {
                        field.Owner = type;
                    }
                }

                if (type is IGraphQlTypesHolder typesHolder)
                {
                    var possibleFields = typesHolder.PossibleTypes
                        .OfType<IGraphQlFieldsHolder>()
                        .SelectMany(t => t.Fields)
                        .DistinctBy(t => t.Name);

                    if (fieldsHolder != null)
                    {
                        possibleFields = possibleFields
                            .Where(pf => fieldsHolder.Fields
                                .All(f => f.Name != pf.Name));
                    }

                    typesHolder.PossibleFields = possibleFields.ToList();
                }
            }
        }


        private static GraphQlObjectType SelectObjectType(GraphQlJsonDto.TypeRef typeRef, IEnumerable<GraphQlTypeBase> types)
        {
            if (typeRef == null)
            {
                return null;
            }

            return (GraphQlObjectType) types.Single(t => t.Name == typeRef.Name);
        }

        private static bool CheckIfSystemType(GraphQlJsonDto.Type type)
        {
            return type.Name.StartsWith("__");
        }



        private class TypeRef : GraphQlTypeBase
        {
            public override GraphQlKind Kind => 0;


            public TypeRef(string name)
            {
                Name = name;
            }
        }
    }
}