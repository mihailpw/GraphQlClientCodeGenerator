using System;
using CommandLine;

namespace GQLCCG
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ConsoleOptions>(args)
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
