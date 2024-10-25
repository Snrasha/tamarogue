using Assets.Scripts.Datas.Enum;
using Assets.Scripts.Datas.Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public ExitType type;


    public ExitStruct GetStruct()
    {
        return new ExitStruct()
        {
            x = (int)transform.localPosition.x,
            y = (int)transform.localPosition.y,
            type = this.type,
        };
    }
}
