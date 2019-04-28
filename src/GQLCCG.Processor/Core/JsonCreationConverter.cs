using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GQLCCG.Processor.Core
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        public override bool CanWrite => false;


        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject;
            try
            {
                jObject = JObject.Load(reader);
            }
            catch (JsonReaderException)
            {
                return null;
            }

            var instance = Create(objectType, jObject);
            if (instance == null)
            {
                throw new NullReferenceException($"Instance created by {GetType().Name} can not be null.");
            }

            serializer.Populate(jObject.CreateReader(), instance);

            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }


        protected abstract T Create(Type objectType, JObject jObject);
    }
}