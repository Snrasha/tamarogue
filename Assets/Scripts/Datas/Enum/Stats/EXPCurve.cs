using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Assets.Scripts.Datas.Enum.Stats
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EXPCurve
	{
		Normal = 0,
		Easy = 1,
		Steep = 2
	}
}
