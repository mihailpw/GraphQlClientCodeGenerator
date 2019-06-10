using System;
using System.Text.RegularExpressions;
using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Utils
{
    public class TypeNames
    {
        public NameEntry DtoEnum { get; set; }
        public NameEntry DtoInputObject { get; set; }
        public NameEntry DtoInterface { get; set; }
        public NameEntry DtoObject { get; set; }
        public NameEntry DtoUnion { get; set; }

        public NameEntry BuilderInterface { get; set; }
        public NameEntry BuilderObject { get; set; }
        public NameEntry BuilderUnion { get; set; }

        public NameEntry ConstructionOnType { get; set; }



        public class NameEntry
        {
            public string RemoveRegex { get; set; }
            public string BuildFormat { get; set; } = "{0}";
        }
    }

    public abstract class TypeNameHelper
    {
        private readonly GeneratorContext _context;


        protected TypeNameHelper(GeneratorContext context)
        {
            _context = context;
        }


        public virtual string ResolveDtoName(GraphQlTypeBase type, bool withNullable)
        {
            TypeNames.NameEntry entry;

            switch (type)
            {
                case GraphQlEnumType _:
                    entry = _context.Names.DtoEnum;
                    break;
                case GraphQlInputObjectType _:
                    entry = _context.Names.DtoInputObject;
                    break;
                case GraphQlInterfaceType _:
                    entry = _context.Names.DtoInterface;
                    break;
                case GraphQlObjectType _:
                    entry = _context.Names.DtoObject;
                    break;
                case GraphQlUnionType _:
                    entry = _context.Names.DtoUnion;
                    break;
                default:
                    throw new NotSupportedException($"Type {type.Kind:G} can not be dto.");
            }

            return ProcessName(type, entry, withNullable);
        }

        public virtual string ResolveGraphQlTypeName(GraphQlTypeBase type)
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

        public virtual string ResolveBuilderName(GraphQlTypeBase type)
        {
            type = type.GetRealType();

            TypeNames.NameEntry entry;

            switch (type)
            {
                case GraphQlInterfaceType _:
                    entry = _context.Names.BuilderInterface;
                    break;
                case GraphQlObjectType _:
                    entry = _context.Names.BuilderObject;
                    break;
                case GraphQlUnionType _:
                    entry = _context.Names.BuilderUnion;
                    break;
                default:
                    throw new InvalidOperationException($"Type {type.Kind:G} can not be a builder.");
            }

            return ProcessName(type, entry);
        }

        public virtual string ResolveOnTypeMethodName(GraphQlTypeBase type)
        {
            return ProcessName(type.GetRealType(), _context.Names.ConstructionOnType);
        }


        protected abstract string MakeNullable(string generatedName, GraphQlTypeBase type);


        private string ProcessName(GraphQlTypeBase type, TypeNames.NameEntry entry, bool isNullable = false)
        {
            var result = type.Name;

            if (!string.IsNullOrEmpty(entry.RemoveRegex))
            {
                result = Regex.Replace(result, entry.RemoveRegex, string.Empty);
            }

            if (entry.BuildFormat.Contains("{0}"))
            {
                result = string.Format(entry.BuildFormat, result);
            }

            if (isNullable)
            {
                result = MakeNullable(result, type);
            }

            return result;
        }
    }
}