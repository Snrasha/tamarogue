
using Assets.Scripts.Datas.Members;
using Assets.Scripts.Datas.Save;

namespace Assets.Scripts.Datas.Struct
{

    [System.Serializable]
    public struct MonsterStatsStructWrapper
    {
        public int Level;
        public int EXP;
        public int MaxEXP;
        public int Weight;
        public int HP;
        public int MaxHP;
        public int TP;
        public int MaxTP;
        public int Strength;
        public int Mind;
        public int Speed;
        public int Wisdom;

        public int Attack;
        public int Defense;
        public string monsterSoul;
        public MonsterStatsStructWrapper(MonsterStats monster)
        {
            monsterSoul = monster.monsterSoul.specie.ToString() ;

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