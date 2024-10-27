using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Assets.Scripts.Datas.Enum.Stats
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Element
	{
		Neutral = 0,
		Water = 1,
		Fire = 2,
		Earth = 3,
		Wind = 4,
		Ice = 5,
		Elec = 6,
		Native = 7,
		Machine = 8,
		Any = 9,
		Null = 10,
		Virtual = 11,
		DarkWater = 12,
		DarkFire = 13,
		DarkEarth = 14,
		DarkWind = 15,
		DarkIce = 16,
		DarkElec = 17,
		DarkNative = 18,
		DarkMachine = 19,
		AnyDark = 20
	}
}