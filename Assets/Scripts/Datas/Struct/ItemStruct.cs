using Assets.Scripts.Datas.Enum;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Datas.Struct
{
    [System.Serializable]
    public struct ItemStruct
    {
        public int x;
        public int y;
        public ItemEnum itemEnum;
        public ItemType itemType;
    }
}