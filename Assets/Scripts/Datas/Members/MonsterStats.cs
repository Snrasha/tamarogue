using Assets.Scripts.Datas.Enum;
using Assets.Scripts.Datas.Enum.Stats;
using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;
using Assets.Scripts.Datas.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas.Members
{

    [Serializable]
    public class MonsterStats
    {
        public static readonly int MaxStat = 255;

        public int Level = 1;
        public int EXP = 0;
		public int HP = 10;
		public int TP = 1;
		public bool IsGlitched;
		public float PercentStats;
		public Passive PassiveOverride;
		public MonsterSoul monsterSoul;

		[NonSerialized]
		public int Weight = 100;
		[NonSerialized]
		public int MaxHP = 10;
		[NonSerialized]
		public int MaxTP = 1;
		[NonSerialized]
		public int Strength = 1;
		[NonSerialized]
		public int Mind = 1;
		[NonSerialized]
		public int Speed = 1;
		[NonSerialized]
        public int Wisdom = 1;


		public List<string> LearnedTechs = new List<string>();
		public MonsterStats(bool isPlayer,string tama)
        {

            if (isPlayer)
            {
                MaxHP = 10;
                HP = MaxHP;
				PercentStats = 1f;
			}
            else
            {
				PercentStats = 0.75f;
			}
            Species spec=(Species)System.Enum.Parse(typeof(Species), tama);
            monsterSoul= SoulGlobal.GetSoul(spec);

			GenerateStats();

			HP = MaxHP;
			TP = MaxTP;
        }

        public MonsterStats(MonsterStatsStructWrapper monster)
        {
            monsterSoul = SoulGlobal.GetSoul((Species)System.Enum.Parse(typeof(Species), monster.monsterSoul));
            Level = monster.Level;
            EXP = monster.EXP;
			HP = monster.HP;
			TP = monster.TP;
			IsGlitched = monster.IsGlitched;
			PassiveOverride = monster.PassiveOverride;
			PercentStats = monster.PercentStats;

			GenerateStats();
		}
		public bool Has(Passive p)
		{
			return Passive == p;
		}
		public Passive Passive
		{
			get
			{
				if (PassiveOverride != 0)
				{
					return PassiveOverride;
				}
				return monsterSoul.Passive;
			}
		}
		//public bool Has(TechEffectType type)
		//{
		//	return GetEffectAmount(type) > 0;
		//}


		public int MaxEXP
		{
			get
			{
				switch (monsterSoul.EXPCurve)
				{
					case EXPCurve.Easy:
						return 10 + Mathf.RoundToInt(1.2f * (float)Level * (float)(Level - 1) * Mathf.Pow(Level, 0.55f));
					case EXPCurve.Normal:
						return 10 + Mathf.RoundToInt(1.1f * (float)Level * (float)(Level - 1) * Mathf.Pow(Level, 0.75f));
					case EXPCurve.Steep:
						return 10 + Mathf.RoundToInt(1f * (float)Level * (float)(Level - 1) * Mathf.Pow(Level, 0.89f));
					default:
						return 999999;
				}
			}
		}
		public bool Has(Tech tech)
		{
			return LearnedTechs.Contains(tech.ToString());
		}

		private float GetMonsterPower(MonsterStats monster)
		{
			float num = 1f;
			float num2 = 1f;
			return ((float)monster.Strength * num + (float)monster.Mind * num2 + 1f) / 2f;
		}
		

		private void CalculateTP()
		{
		    MaxTP= Mathf.CeilToInt(2f * Mathf.Sqrt(0.9f * (float)Speed) - 1f)*Level+5;
		}
		public float GetBaseAttackPower(int powerVariance = 0)
		{
			int num = Mathf.RoundToInt(0.33f * (float)(5 + powerVariance + 1) * GetMonsterPower(this));
			float num2 = 1f;
			if (Has(Passive.Neutrality))
			{
				num2 *= 1.3f;
			}
			num2 *= Mathf.Clamp((float)Weight / 200f, 0.75f, 1.75f);
			num2 *= 1f + 0.01f * (float)Level;
			return Mathf.Max(1f, (float)num * num2);
		}

		public void GenerateStats()
		{
			Strength = 1;
			Speed = 1;
			Mind = 1;
			Wisdom = 1;
			Weight = monsterSoul.BaseWeight;
			MaxHP = (int)monsterSoul.BaseHP;

			// for weak monsters.
			for (int i = 1; i < Level; i++)
			{
				LevelUpStats();
			}
			if (IsGlitched)
			{
				for (int i = 1; i < 5; i++)
				{
					LevelUpStats();
				}
			}
			Strength = (int)(Strength * PercentStats);
			Speed = (int)(Speed * PercentStats);
			Mind = (int)(Mind * PercentStats);
			Wisdom = (int)(Wisdom * PercentStats);

			MaxHP = (int)(MaxHP * PercentStats);

			if (MaxHP < 30)
			{
				MaxHP = GetLevelUpMaxHP();
			}
			//HP = MaxHP;
			CalculateTP();
		}
		private void LevelUpStats()
		{
			float num = 1f;
			MaxHP = GetLevelUpMaxHP();
			Strength += (int)(monsterSoul.Affinity.Strength.PowIfLessThanOne(1.75f) * num);
			Mind += (int)(monsterSoul.Affinity.Magic.PowIfLessThanOne(1.75f) * num);
			Wisdom += (int)(monsterSoul.Affinity.Wisdom.PowIfLessThanOne(1.5f) * num);
			Speed += (int)(monsterSoul.Affinity.Speed.PowIfLessThanOne(1.5f) * num);
		}
		private int GetLevelUpMaxHP(int points = 1)
		{
			int num = MaxHP;
			for (int i = 0; i < points; i++)
			{
				int value = Mathf.FloorToInt(((num < 80) ? 0.5f : ((num < 160) ? 0.4f : 0.3f)) * (monsterSoul.Affinity.HP * (float)num).Pow(0.69f));
				num += Mathf.Clamp(value, 0, 99);
			}
			return num;
		}
	}
}