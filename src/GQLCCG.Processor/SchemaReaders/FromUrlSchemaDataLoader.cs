using System.Net.Http;
using System.Threading.Tasks;
using GQLCCG.Infra.Utils;

namespace GQLCCG.Processor.SchemaReaders
{
    public class FromUrlSchemaDataLoader : ISchemaDataLoader
    {
        private readonly string _uri;


        public FromUrlSchemaDataLoader(string uri)
        {
            _uri = uri.VerifyNotNull(nameof(uri));
        }


        public async Task<string> LoadSchemaDataAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_uri);
                var contentString = await response.Content.ReadAsStringAsync();

                return contentString;
            }
        }
    }
}