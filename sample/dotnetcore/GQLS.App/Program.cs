using System;
using System.Threading.Tasks;
using GraphQlClient;
using Newtonsoft.Json;

namespace GQLS.App
{
    internal class Program
    {
        private const string GraphQlUrl = "https://swapi.apis.guru";


        public static async Task Main(string[] args)
        {
            var clientFactory = new AppApiClientFactory(GraphQlUrl);

            var data = await clientFactory.CreateQueryClient(r => r
                .AllFilms()(fc => fc
                    .TotalCount()
                    .Films()(f => f
                        .Director()
                        .Created()
                        .Id()
                        .Title())))
                .SendAsync();

            WriteJsonToConsole(data);
            Console.ReadLine();
        }


        private static void WriteJsonToConsole(object value)
        {
            Console.WriteLine(JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            }));
        }
    }
}
