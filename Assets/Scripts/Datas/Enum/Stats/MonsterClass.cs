using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Assets.Scripts.Datas.Enum.Stats
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MonsterClass
	{
		None = 0,
		Bird = 1,
		Amphibian = 2,
		Lizard = 3,
		Snake = 4,
		Plant = 5,
		Insect = 6,
		Wolf = 7,
		Fox = 8,
		Mouse = 9,
		Bear = 10,
		Rabbit = 11,
		Crab = 12,
		Fish = 13,
		Phantasm = 14,
		Mechanica = 15,
		Fairy = 16,
		Man = 17,
		Mythical = 18
	}
}