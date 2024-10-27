using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Assets.Scripts.Datas.Enum.Stats
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MonsterDiet
	{
		Omnivore = 0,
		Herbivore = 1,
		Carnivore = 2,
		Metalvore = 3
	}
}