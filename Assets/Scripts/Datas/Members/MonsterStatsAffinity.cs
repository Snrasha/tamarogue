using System;
namespace Assets.Scripts.Datas.Members
{
	[Serializable]
	public class MonsterStatsAffinity
	{
		public float HP = 1f;

		public float Strength = 1f;

		public float Magic = 1f;

		public float Wisdom = 1f;

		public float Speed = 1f;

		public float Sum
		{
			get
			{
				return HP + Strength + Magic + Speed + Wisdom;
			}
		}
	}

}