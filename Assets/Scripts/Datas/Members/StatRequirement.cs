using Assets.Scripts.Datas.Members;
using System;

namespace Assets.Scripts.Datas.Members
{

	[Serializable]
	public class StatRequirement
	{
		public int min = int.MinValue;

		public int max = int.MaxValue;

		public static StatRequirement ANY = new StatRequirement(int.MinValue, int.MaxValue);

		public bool isRequirement
		{
			get
			{
				if (min < 1)
				{
					return max <= MonsterStats.MaxStat;
				}
				return true;
			}
		}

		public int required
		{
			get
			{
				if (min < 1)
				{
					return max;
				}
				return min;
			}
		}

		public string RequiredTextId
		{
			get
			{
				if (min < 1)
				{
					return "Require more stats";
				}
				return "Require less stats";
			}
		}

		public StatRequirement(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		public bool Check(int stat)
		{
			if (stat >= min)
			{
				return stat <= max;
			}
			return false;
		}
	}

}