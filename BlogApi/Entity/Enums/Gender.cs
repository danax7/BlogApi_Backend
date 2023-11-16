using System.Text.Json.Serialization;

namespace BlogApi.Entity.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Female,
    Male
}