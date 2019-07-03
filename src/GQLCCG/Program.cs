using System;
using System.Collections.Generic;
using CommandLine;

namespace GQLCCG
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "GraphQL Client Code Generator";

            var parser = new Parser(o =>
            {
                o.AutoHelp = true;
                o.AutoVersion = false;
                o.IgnoreUnknownArguments = true;
                o.HelpWriter = Console.Out;
            });

            parser.ParseArguments<ConsoleOptions>(args)
                .WithParsed(GenerateClient)
                .WithNotParsed(HandleErrors);
        }


        private static void GenerateClient(ConsoleOptions options)
        {
            try
            {
                ClientGeneration.GenerateClientAsync(options).Wait();
            }
            catch (Exception e)
            {
                var exception = e.InnerException ?? e;

#if DEBUG
                Console.WriteLine(exception);
#else
                Console.WriteLine($"Error: {exception.Message}");
#endif

                Environment.ExitCode = -1;
            }
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine("Arguments parsing errors.");
            Environment.ExitCode = -1;
        }
    }
}
