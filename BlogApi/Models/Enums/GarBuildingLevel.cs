using System.ComponentModel;

namespace BlogApi.Entity.Enums;

public enum GarBuildingLevel
{
    [Description("Здание")] Building,
    [Description("Комната")] Room,
    [Description("Комната в комнатах")] RoomInRooms,
    [Description("Парковочное место")] CarPlace
}