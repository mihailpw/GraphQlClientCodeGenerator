using System;
using System.Collections.Generic;
using CommandLine;

namespace GQLCCG
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
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
                Console.WriteLine(e.InnerException ?? e);
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
