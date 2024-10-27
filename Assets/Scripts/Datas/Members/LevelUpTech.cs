using Assets.Scripts.Datas.Enum;
using System;
namespace Assets.Scripts.Datas.Members
{

	[Serializable]
	public class LevelUpTech
	{
		public Tech Tech;

		public int Level;

		public LevelUpTech(Tech tech, int level)
		{
			Tech = tech;
			Level = level;
		}
	}

}