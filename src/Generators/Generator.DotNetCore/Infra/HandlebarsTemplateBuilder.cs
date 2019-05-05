using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Generator.DotNetCore.Helpers;
using GQLCCG.Infra;
using GQLCCG.Infra.Models.Types;
using HandlebarsDotNet;

namespace Generator.DotNetCore.Infra
{
    public class HandlebarsTemplateBuilder : ITemplateBuilder
    {
        public HandlebarsTemplateBuilder(GeneratorContext context)
        {
            var nameResolver = new NameResolver(context);

            Handlebars.RegisterHelper("resolveDtoName", (output, _, arguments) => output.Write(nameResolver.ResolveDtoName(GetFromArguments(arguments))));
            Handlebars.RegisterHelper("resolveBuilderName", (output, _, arguments) => output.Write(nameResolver.ResolveBuilderName(GetFromArguments(arguments))));
            Handlebars.RegisterHelper("resolveGraphQlType", (output, _, arguments) => output.Write(nameResolver.ResolveGraphQlType(GetFromArguments(arguments))));
            Handlebars.RegisterHelper("toPascal", ToPascal);
        }


        public Task<string> BuildAsync(string template, object data, IDictionary<string, string> partials = null)
        {
            if (partials != null)
            {
                foreach (var partial in partials)
                {
                    Handlebars.RegisterTemplate(partial.Key, partial.Value);
                }
            }

            var builder = Handlebars.Compile(template);
            var result = builder(data);

            return Task.FromResult(result);
        }


        private static void ToPascal(TextWriter output, object context, object[] arguments)
        {
            var value = string.Concat(arguments);
            var pascalValue = value.ToPascal();
            output.Write(pascalValue);
        }

        private static GraphQlTypeBase GetFromArguments(IReadOnlyList<object> arguments)
        {
            if (arguments.Count != 1)
            {
                throw new InvalidOperationException("Should be 1 argument: type object.");
            }
            if (!(arguments[0] is GraphQlTypeBase type))
            {
                throw new InvalidOperationException("Should be type object.");
            }

            return type;
        }
    }
}