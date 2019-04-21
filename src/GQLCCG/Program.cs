using System;
using CommandLine;

namespace GQLCCG
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var parser = new Parser(o =>
            {
                o.AutoHelp = false;
                o.AutoVersion = false;
            });

            parser.ParseArguments<ConsoleOptions>(args)
                .WithParsed(GenerateClient);

            Console.WriteLine();
            Console.Write("Execution finished. Press <enter> to exit...");
            Console.ReadLine();
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
            }
        }
    }
}
