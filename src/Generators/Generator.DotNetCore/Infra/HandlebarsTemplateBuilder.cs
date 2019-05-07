using System;
using System.Collections.Generic;
using System.Linq;
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
            var typeHelper = new GraphQlTypeHelper(context);

            foreach (var methodInfo in typeof(GraphQlTypeHelper)
                .GetMethods()
                .Where(m => m
                    .CustomAttributes
                    .Any(a => a.AttributeType == typeof(GraphQlTypeHelper.AutoWire))))
            {
                if (methodInfo.ReturnType == typeof(string))
                {
                    Handlebars.RegisterHelper(
                        methodInfo.Name,
                        (output, _, args) => output.Write((string)methodInfo.Invoke(typeHelper, args)));
                }
                else if (methodInfo.ReturnType == typeof(bool))
                {
                    Handlebars.RegisterHelper(
                        methodInfo.Name,
                        (output, opt, ctx, args) =>
                        {
                            if ((bool) methodInfo.Invoke(typeHelper, args))
                            {
                                opt.Template(output, ctx);
                            }
                            else
                            {
                                opt.Inverse(output, ctx);
                            }
                        });
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            Handlebars.RegisterHelper(
                "ifKind",
                (output, opt, ctx, args) =>
                {
                    var resolvedArgs = args.ResolveArgumentsInfinity<GraphQlTypeBase, string>();
                    if (typeHelper.IfKind(resolvedArgs.first, resolvedArgs.second))
                    {
                        opt.Template(output, ctx);
                    }
                    else
                    {
                        opt.Inverse(output, ctx);
                    }
                });

            Handlebars.RegisterHelper(
                "withDocs",
                (output, opt, ctx, args) =>
                {
                    if (context.GenerateDocs)
                    {
                        opt.Template(output, ctx);
                    }
                    else
                    {
                        opt.Inverse(output, ctx);
                    }
                });

            Handlebars.RegisterHelper(
                "toPascal",
                (output, _, args) => output.Write(args.ResolveArgument<string>().ToPascal()));
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
    }
}