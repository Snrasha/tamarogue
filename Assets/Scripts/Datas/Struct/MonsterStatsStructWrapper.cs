
using Assets.Scripts.Datas.Enum.Stats;
using Assets.Scripts.Datas.Members;
using Assets.Scripts.Datas.Save;

namespace Assets.Scripts.Datas.Struct
{

    [System.Serializable]
    public struct MonsterStatsStructWrapper
    {
        public int Level;
        public int EXP;
        public bool IsGlitched;
        public string monsterSoul;
        public int HP;
        public int TP ;
        public Passive PassiveOverride;
        public float PercentStats;
        public MonsterStatsStructWrapper(MonsterStats monster)
        {
            monsterSoul = monster.monsterSoul.specie.ToString() ;
            Level = monster.Level;
            EXP = monster.EXP;
            IsGlitched = monster.IsGlitched;
            HP = monster.HP;
            TP = monster.TP;
            PassiveOverride = monster.PassiveOverride;
            PercentStats = monster.PercentStats;

        }
    }
}