using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace GQLCCG.Processor.Core
{
    public abstract class JsonListItemTypeInferringConverter<T> : JsonConverter
    {
        public override bool CanWrite => false;


        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!(serializer.ContractResolver.ResolveContract(objectType) is JsonArrayContract contract) 
                || contract.IsMultidimensionalArray
                || objectType.IsArray)
            {
                throw new JsonSerializationException($"Invalid array contract for {objectType}");
            }

            if (MoveToContent(reader).TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType != JsonToken.StartArray)
            {
                throw new JsonSerializationException($"Expected {JsonToken.StartArray:G}, encountered {reader.TokenType} at path {reader.Path}");
            }

            var collection = existingValue as IList<T> ?? (IList<T>) contract.DefaultCreator();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.EndArray:
                        return collection;
                    case JsonToken.Comment:
                        break;
                    default:
                    {
                        var token = JToken.Load(reader);
                        if (TryInferItemType(typeof(T), token, out var itemType))
                        {
                            collection.Add((T) serializer.Deserialize(token.CreateReader(), itemType));
                        }

                        break;
                    }
                }
            }

            throw new JsonSerializationException($"Unclosed array at path: {reader.Path}");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }


        protected abstract bool TryInferItemType(Type objectType, JToken jToken, out Type type);


        private static JsonReader MoveToContent(JsonReader reader)
        {
            while ((reader.TokenType == JsonToken.Comment || reader.TokenType == JsonToken.None) && reader.Read())
            {
            }

            return reader;
        }
    }
}