using Assets.Scripts.Datas.Enum;
using System;
namespace Assets.Scripts.Datas.Members
{

	[Serializable]
	public class MonsterDrop
	{
		public ItemEnum Item;

		public float Chance;

		public MonsterDrop(ItemEnum item, float chance)
		{
			Item = item;
			Chance = chance;
		}
	}

}