using System.Globalization;
using Newtonsoft.Json;
using Nucleus.Utilities;

namespace Nucleus.Api.JsonConverters;

public class CrypticJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    ///  deserilization
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        objectType = Nullable.GetUnderlyingType(objectType) ?? objectType;

        if (reader?.ValueType is not null && reader.Value!.ToString()!.IsValidBase64String())
        {
            var decryptedValue = reader.Value!.ToString()!.Decrypt();

            return !string.IsNullOrWhiteSpace(decryptedValue)
            ? Convert.ChangeType(reader.Value!.ToString()!.Decrypt(), objectType, CultureInfo.InvariantCulture) : decryptedValue;
        }

        return string.IsNullOrWhiteSpace(Convert.ToString(reader?.Value!, CultureInfo.InvariantCulture)) ? reader?.Value :
        Convert.ChangeType(reader?.Value!, objectType, CultureInfo.InvariantCulture);
    }


    /// <summary>
    ///  serilization 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    /// <exception cref="NotImplementedException"></exception>
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is not null) writer?.WriteValue(value.IsZero() ? value : value.ToString()?.Encrypt());
        else writer?.WriteValue(value);
    }
}
