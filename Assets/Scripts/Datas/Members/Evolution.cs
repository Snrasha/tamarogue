using Assets.Scripts.Datas.Enum.Stats;
using Assets.Scripts.Datas.Members;
using System;

[Serializable]
public class Evolution
{
	public Species Species;

	public int Level;

	public StatRequirement Strength = StatRequirement.ANY;

	public StatRequirement Speed = StatRequirement.ANY;

	public StatRequirement Mind = StatRequirement.ANY;

	public StatRequirement Magic = StatRequirement.ANY;

	public StatRequirement Wisdom = StatRequirement.ANY;

	public StatRequirement Weight = StatRequirement.ANY;

	public Evolution(Species s, int level)
	{
		Species = s;
		Level = level;
	}
	public bool MeetsLevelRequirement(MonsterStats monster)
	{
		if (monster == null)
		{
			return false;
		}
		return monster.Level >= Level;
	}

	public bool MeetsNonLevelRequirements(MonsterStats monster)
	{
		if (monster == null)
		{
			return false;
		}
		if (Strength.Check(monster.Strength) && Speed.Check(monster.Speed) && Mind.Check(monster.Mind) && Magic.Check(monster.Mind) && Wisdom.Check(monster.Wisdom))
		{
			return Weight.Check(monster.Weight);
		}
		return false;
	}

	public bool MeetsRequirements(MonsterStats monster)
	{
		if (MeetsLevelRequirement(monster))
		{
			return MeetsNonLevelRequirements(monster);
		}
		return false;
	}
}
