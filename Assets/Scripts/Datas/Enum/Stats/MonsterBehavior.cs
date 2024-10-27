using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Assets.Scripts.Datas.Enum.Stats
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MonsterBehavior
	{
		Stationary = 0,
		Slow = 1,
		Patrol = 2,
		Aggressive = 3,
		SlowAggressive = 4,
		PatrolAggressive = 5,
		StationaryAggressive = 6
	}

}