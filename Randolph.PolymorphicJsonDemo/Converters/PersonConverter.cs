using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Randolph.PolymorphicJsonDemo.Constants;
using Randolph.PolymorphicJsonDemo.Models;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace Randolph.PolymorphicJsonDemo.Converters;

public class PersonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var token = JToken.FromObject(value);

        JObject destinationObject = (JObject)token;
        
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var person = new Person();
        
        while (reader.Read())
        {
            if (reader.TokenType is JsonToken.StartObject or JsonToken.EndObject)
            {
                continue;
            }

            if (reader.TokenType == JsonToken.PropertyName)
            {
                string propertyName = (string)reader.Value;

                switch (propertyName)
                {
                    case "firstName":
                        person.FirstName = reader.ReadAsString();
                        break;
                    
                    case "lastName":
                        person.LastName = reader.ReadAsString();
                        break;
                    
                    case "insuranceKinds":
                        person.InsuranceKinds = ConvertInsuranceKinds(reader);
                        break;
                }
            }
        }

        return person;
    }

    public override bool CanConvert(Type objectType) => objectType == typeof(Person);

    private InsuranceKind[] ConvertInsuranceKinds(JsonReader reader)
    {
        List<InsuranceKind> insuranceKinds = new List<InsuranceKind>();

        void ParseInsuranceKindFromReader(JsonReader r)
        {
            string? val = r.ReadAsString();

            if (val == null)
            {
                return;
            }
            
            var ik = Enum.Parse<InsuranceKind>(val!);
            insuranceKinds.Add(ik);
        }

        reader.Read();
        if (reader.TokenType == JsonToken.StartArray)
        {
            do
            {
                ParseInsuranceKindFromReader(reader);
            } while (reader.TokenType != JsonToken.EndArray);
        }
        else
        {
            ParseInsuranceKindFromReader(reader);
        }

        return insuranceKinds.ToArray();
    }
}