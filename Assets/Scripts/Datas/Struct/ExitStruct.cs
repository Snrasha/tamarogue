using Assets.Scripts.Datas.Enum;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Datas.Struct
{
    [System.Serializable]
    public struct ExitStruct
    {
        public int x;
        public int y;
        public ExitType type;
    }
}