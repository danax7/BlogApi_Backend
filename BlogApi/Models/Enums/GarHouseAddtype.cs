using System.ComponentModel;

namespace BlogApi.Entity.Enums;

public enum GarHouseAddtype
{
   
    [Description("Сооружение")] Construction,
    [Description("к.")] Corps,
    [Description("стр.")] Building,
    [Description("Литера")] Litera,
}