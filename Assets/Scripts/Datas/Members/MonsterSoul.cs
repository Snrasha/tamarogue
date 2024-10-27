using Assets.Scripts.Datas.Enum;
using Assets.Scripts.Datas.Enum.Stats;
using Assets.Scripts.Datas.Utils;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Datas.Members
{
    [Serializable]

    public class MonsterSoul
    {
        public Element Element;

        public float BaseHP = 10f;

        public int BaseWeight = 100;

        public MonsterStatsAffinity Affinity = new MonsterStatsAffinity();

        public List<LevelUpTech> Techs = new List<LevelUpTech>();

        public List<Evolution> Evolutions = new List<Evolution>();

        public Species BaseAncestor = Species.Averat;

        public MonsterDiet Diet;

        public MonsterClass Class = MonsterClass.Wolf;

        public MonsterBehavior Behavior;

        public List<MonsterDrop> Drops = new List<MonsterDrop>();

        public ItemEnum FavoriteItem = ItemEnum.Fiber;

        public Species specie;

        public Passive Passive;

        public EXPCurve EXPCurve;

        public string Spot = "";

        public bool NoShadow;

        public bool Flying;

        public bool IsAsm;

     //   public SerializableDictionary<string, int> LearnedTechsSortOrder = new SerializableDictionary<string, int>();

     //   public SerializableDictionary<string, int> ElementAffinity = new SerializableDictionary<string, int>();

    }
}