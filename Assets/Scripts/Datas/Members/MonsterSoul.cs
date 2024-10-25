using Assets.Scripts.Datas.Utils;
using System;

namespace Assets.Scripts.Datas.Members
{
    [Serializable]

    public class MonsterSoul
    {
        public Species specie;

        public MonsterStatsAffinity statsAffinity;

        public SerializableDictionary<string, int> LearnedTechsSortOrder = new SerializableDictionary<string, int>();

        public SerializableDictionary<string, int> ElementAffinity = new SerializableDictionary<string, int>();

        public MonsterSoul(Species specie)
        {
            this.specie = specie;
            // Load species
        }

    }
}