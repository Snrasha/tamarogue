using Assets.Scripts.Datas.Members;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] private int levelUpBase;
    [SerializeField] private float levelUpModifier;
    [SerializeField] private int xpNextLevel;

    void Start()
    {
        levelUpBase = 10;
        levelUpModifier = 0.1f;

        xpNextLevel = 11;
    }

    public void AddXP(MonsterStats monsterStats,int _xp)
    {
        monsterStats.EXP = monsterStats.EXP + _xp;
        CheckIfLevelUp(monsterStats);
    }

    private void CalculateXPNextLevel(MonsterStats monsterStats)
    {
        xpNextLevel = (int)(levelUpBase * (monsterStats.Level * (1+ levelUpModifier)));
    }

    private void CheckIfLevelUp(MonsterStats monsterStats)
    {
        if (monsterStats.EXP > xpNextLevel)
        {
            // Level up, reset the current xp, and calculate new xp required to level up again
            monsterStats.Level++;
            monsterStats.MaxEXP = monsterStats.MaxEXP + monsterStats.EXP;
            monsterStats.EXP -= xpNextLevel;
            MessageLogManager.Instance.AddToQueue("You reached level " + monsterStats.Level + "!");
            CalculateXPNextLevel(monsterStats);
        }
    }

}
