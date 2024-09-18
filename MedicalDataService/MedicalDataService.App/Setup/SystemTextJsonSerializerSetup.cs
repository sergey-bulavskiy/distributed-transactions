using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalDataService.Setup;

public static class SystemTextJsonSerializerSetup
{
    public static JsonOptions SetupJson(this JsonOptions options)
    {
        options.JsonSerializerOptions.Converters.Add(new JsonNetDateTimeUtcConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        var deserializationOptions = new JsonSerializerOptions(options.JsonSerializerOptions);

        return options;
    }
}
