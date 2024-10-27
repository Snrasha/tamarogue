using Assets.Scripts.Datas.Enum.Stats;
using Assets.Scripts.Datas.Members;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Level a besoin d'être global, pas lié à l'entity player. Pour faire gagner de l'exp pour tous les tamas.
// Mais après, si tu n'as aucun tamas qui te suit, vu que on veut rajouter des salles piégés avec des balistes qui tirent des flèches ?
public class LevelSystem : MonoBehaviour
{

    private List<Entity> entities;

    private int maxLevelInParty;



    public void Init()
    {
        entities= Engine.Instance.playerGenerator.monstersTeam;
        maxLevelInParty = 1;
        foreach (Entity entity in entities)
        {
            maxLevelInParty = Mathf.Max(entity.monsterStats.Level, maxLevelInParty);
        }

    }



    public void AddXP(MonsterStats enemi_defeated)
    {
        bool updateStatus = false;
        MessageLogManager.Instance.AddToQueue("Team gain EXP !");

        foreach (Entity entity in entities)
        {
            int expgain= AllExp(entity.monsterStats, enemi_defeated);

            entity.monsterStats.EXP += expgain;
            if (entity.monsterStats.EXP >= entity.monsterStats.MaxEXP)
            {
                entity.monsterStats.EXP -= entity.monsterStats.MaxEXP;
                entity.monsterStats.Level++;
                entity.monsterStats.GenerateStats();
                maxLevelInParty = Mathf.Max(entity.monsterStats.Level, maxLevelInParty);
                updateStatus = true;
                MessageLogManager.Instance.AddToQueue(entity.monsterStats.monsterSoul.specie.ToString()+ " reached level " + entity.monsterStats.Level + "!");
            }
        }
        if (updateStatus)
        {
            Engine.Instance.statusManager.UpdateUI();
        }
    }

    private int CalculateEXPYield(MonsterStats player, MonsterStats enemi)
    {
        float num = Mathf.Pow(1.07f, Mathf.Clamp(enemi.Level - player.Level, -3, 3));
        float num2 = 1f + ((enemi.Level >= 10) ? 1f : 0f) + ((enemi.Level >= 20) ? 1.5f : 0f) + ((enemi.Level >= 30) ? 1f : 0f);
        float num3 = ((enemi.monsterSoul.EXPCurve == EXPCurve.Steep) ? 1.2f : 1f);
        float num4 = Mathf.Sqrt(Mathf.Pow(enemi.Level, 1.1f - 0.002f * (float)player.Level) * (float)(enemi.MaxHP + 4 * (enemi.Strength + enemi.Mind + enemi.Speed + enemi.Wisdom)));
        float num5 = 1f;
        float num6 = 1f;
        float num7= 1f;
        return Mathf.RoundToInt(num5 * num * num2 * num3 * num4 * num6 * num7);
    }

    // Besoin de changer, pour que tout le monde gagne de l'exp.
    public int AllExp(MonsterStats player,MonsterStats enemi)
    {
        float num = CalculateEXPYield(player,enemi);

        float num2 = ((player.monsterSoul.Passive == Passive.FastLearner) ? 1.5f : 1f);
        num = Mathf.Max(1, (int)((float)num * num2));
        return Mathf.RoundToInt((float)num * (1f + Mathf.Clamp(0.1f * (float)(maxLevelInParty - enemi.Level), 0f, 1f)));
    }

}
