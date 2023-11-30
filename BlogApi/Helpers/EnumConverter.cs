namespace BlogApi.Helpers;

using System;

public static class EnumExtensions
{
    public static string GetEnumAsString(this Enum value)
    {
        return Enum.GetName(value.GetType(), value);
    }
}