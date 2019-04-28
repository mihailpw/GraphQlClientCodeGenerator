using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GQLCCG.Infra.Models;
using GQLCCG.Infra.Utils;
using GQLCCG.Processor.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GQLCCG.Processor.SchemaReaders
{
    public class FromUrlSchemaReader : ISchemaReader
    {
        private readonly string _uri;


        public FromUrlSchemaReader(string uri)
        {
            _uri = uri.VerifyNotNull(nameof(uri));
        }


        public async Task<GraphQlSchema> LoadSchemaDataAsync()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsync(_uri, new StringContent(JsonConvert.SerializeObject(new { query = GraphQlJsonDto.RetrieveSchemaQuery }), Encoding.UTF8, "application/json")))
                {
                    var content = response.Content != null
                        ? await response.Content.ReadAsStringAsync()
                        : "<null>";
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(
                            $"Status code: {response.StatusCode} ({response.StatusCode}); content: {content}");
                    }

                    try
                    {
                        var jObject = (JObject) JsonConvert.DeserializeObject(content);
                        var schemeJObject = jObject["data"]["__schema"];
                        var dto = schemeJObject.ToObject<GraphQlJsonDto.Schema>();
                        var schema = ConvertFromDto(dto);

                        return schema;
                    }
                    catch (JsonReaderException e)
                    {
                        throw new JsonReaderException("Invalid schema json received.", e);
                    }
                }
            }
        }


        private static GraphQlSchema ConvertFromDto(GraphQlJsonDto.Schema dto)
        {
            return new GraphQlSchema();
        }
    }
}