using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;
using System;

namespace Assets.Scripts.Datas.Members
{

    [Serializable]
    public class MonsterStats
    {
        public int Level = 1;
        public int EXP = 0;
        public int MaxEXP = 0;
        public int Weight = 100;
        public int HP = 10;
        public int MaxHP = 10;
        public int TP = 1;
        public int MaxTP = 1;
        public int Strength = 1;
        public int Mind = 1;
        public int Speed = 1;
        public int Wisdom = 1;


        public int Attack;
        public int Defense;

        public MonsterSoul monsterSoul;


        public MonsterStats(bool isPlayer,string tama)
        {
            // Base stats:
            Attack = 10;
            Defense = 10;
            MaxHP = 3;
            HP = MaxHP;
            MaxTP = 10;
            TP = MaxTP;
            EXP = 5; // xp an entity gives when dies
            if (isPlayer)
            {
                MaxHP = 10;
                HP = MaxHP;
                EXP = 0; // player xp
            }
            Species spec=(Species)System.Enum.Parse(typeof(Species), tama);
            monsterSoul = new MonsterSoul(spec);
        }

        public MonsterStats(MonsterStatsStructWrapper monster)
        {
            monsterSoul = new MonsterSoul((Species)System.Enum.Parse(typeof(Species), monster.monsterSoul));
            Level = monster.Level;
            EXP = monster.EXP;
            MaxEXP = monster.MaxEXP;
            Weight = monster.Weight;
            HP = monster.HP;
            MaxHP = monster.MaxHP;
            TP = monster.TP;
            MaxTP = monster.MaxTP;
            Strength = monster.Strength;
            Mind = monster.Mind;
            Speed = monster.Speed;
            Wisdom = monster.Wisdom;

            Attack = monster.Attack;
            Defense = monster.Defense;
        }




    }
}