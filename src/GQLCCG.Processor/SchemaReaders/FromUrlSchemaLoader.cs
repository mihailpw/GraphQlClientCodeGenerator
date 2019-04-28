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
    public class FromUrlSchemaLoader : ISchemaLoader
    {
        private readonly string _uri;


        public FromUrlSchemaLoader(string uri)
        {
            _uri = uri.VerifyNotNull(nameof(uri));
        }


        public async Task<GraphQlSchema> LoadSchemaDataAsync()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsync(_uri, new StringContent(CreateRequestBody(), Encoding.UTF8, "application/json")))
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
                        var result = schemeJObject.ToObject<GraphQlSchema>(JsonSerializer.Create(CreateSerializerSettings()));

                        return result;
                    }
                    catch (JsonReaderException e)
                    {
                        throw new JsonReaderException("Invalid schema json received.", e);
                    }
                }
            }
        }


        private static string CreateRequestBody()
        {
            return JsonConvert.SerializeObject(new
            {
                query = "query IntrospectionQuery { __schema { queryType { name } mutationType { name } subscriptionType { name } types { ...FullType } directives { name description locations args { ...InputValue } } } } fragment FullType on __Type { kind name description fields(includeDeprecated: true) { name description args { ...InputValue } type { ...TypeRef } isDeprecated deprecationReason } inputFields { ...InputValue } interfaces { ...TypeRef } enumValues(includeDeprecated: true) { name description isDeprecated deprecationReason } possibleTypes { ...TypeRef } } fragment InputValue on __InputValue { name description type { ...TypeRef } defaultValue } fragment TypeRef on __Type { kind name ofType { kind name ofType { kind name ofType { kind name } } } }"
            });
        }

        private static JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new SkipSystemGraphQlTypesConverter());
            settings.Converters.Add(new GraphQlTypeConverter());

            return settings;
        }



        private class SkipSystemGraphQlTypesConverter : JsonListItemTypeInferringConverter<GraphQlType>
        {
            protected override bool TryInferItemType(Type objectType, JToken jToken, out Type type)
            {
                var name = jToken["name"].ToString();
                if (name.StartsWith("__"))
                {
                    type = null;
                    return false;
                }

                type = typeof(GraphQlType);
                return true;
            }
        }

        private class GraphQlTypeConverter : JsonCreationConverter<GraphQlType>
        {
            protected override GraphQlType Create(Type objectType, JObject jObject)
            {
                var kind = jObject["kind"]?.ToString();
                var type = GetGraphQlTypeType(kind);
                var result = (GraphQlType) jObject.ToObject(type);

                return result;
            }


            private static Type GetGraphQlTypeType(string kind)
            {
                if (string.IsNullOrEmpty(kind))
                {
                    return typeof(GraphQlType);
                }

                switch (kind)
                {
                    case "SCALAR":
                        return typeof(GraphQlScalarType);
                    case "OBJECT":
                        return typeof(GraphQlObjectType);
                    case "INTERFACE":
                        return typeof(GraphQlInterfaceType);
                    case "INPUT_OBJECT":
                        return typeof(GraphQlInputObjectType);
                    case "ENUM":
                        return typeof(GraphQlEnumType);
                    case "LIST":
                        return typeof(GraphQlListType);
                    case "NON_NULL":
                        return typeof(GraphQlListType);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
                }
            }
        }
    }
}