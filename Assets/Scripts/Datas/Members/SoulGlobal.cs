
using Assets.Scripts.Datas.Enum.Stats;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Datas.Members
{
    public static class SoulGlobal
    {
        private static Dictionary<Species, MonsterSoul> souls = new Dictionary<Species, MonsterSoul>();

        public static MonsterSoul GetSoul(Species tama)
        {

            MonsterSoul soul;
            if (souls.ContainsKey(tama))
            {
                return souls[tama];
            }
            else
            {
                //  Debug.Log("GetIconTama " + tama);
                Debug.Log("Tamas/" + tama.ToString() + "/Stats");
                TextAsset test = Resources.Load<TextAsset>("Tamas/" + tama.ToString() + "/Stats");
                if (test == null)
                {
                    return GetSoul(Species.Kobou);
                }
                else
                {

                    soul = JsonUtility.FromJson<MonsterSoul>(test.text);
                    soul.specie = tama;
                    Debug.Log(soul.Affinity.ToString());
                    souls[tama] = soul;
                }

            }
            return soul;
        }
    }

}