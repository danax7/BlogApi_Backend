using System.ComponentModel;

namespace BlogApi.Entity.Enums;

public enum GarAddressLevel
{
    [Description("Страна")] Country,
    [Description("Регион")] Region,

    [Description("Административный район")]
    AdministrativeArea,
    [Description("Муниципальный район")] MunicipalArea,

    [Description("Сельско-городская территория")]
    RuralUrbanSettlement,
    [Description("Город")] City,
    [Description("Населенный пункт")] Locality,

    [Description("Элемент планировочной структуры")]
    ElementOfPlanningStructure,
    [Description("Элемент дорожной сети")] ElementOfRoadNetwork,
    [Description("Земельный участок")] Land,
    [Description("Здание")] Building,
    [Description("Комната")] Room,
    [Description("Комната в комнатах")] RoomInRooms,

    [Description("Автономный уровень региона")]
    AutonomousRegionLevel,
    [Description("Городской уровень")] IntracityLevel,

    [Description("Дополнительные территории")]
    AdditionalTerritoriesLevel,

    [Description("Уровень объектов на дополнительных территориях")]
    LevelOfObjectsInAdditionalTerritories,
    [Description("Парковочное место")] CarPlace
}