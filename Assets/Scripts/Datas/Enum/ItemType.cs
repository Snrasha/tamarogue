using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Assets.Scripts.Datas.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ItemType
    {
        None,
        Consumable,
        Exit,
    }
}
