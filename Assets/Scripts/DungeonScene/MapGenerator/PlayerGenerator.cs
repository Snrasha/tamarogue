using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public List<Entity> monstersTeam = new List<Entity>();

    public Transform teamsParent;



    public Entity HasPlayerTeam(int x,int y)
    {
        foreach (Entity player in monstersTeam)
        {
            if (player.gameObject.activeSelf && player.x == x && player.y == y)
            {
                return player;
            }
        }
        return null;
    }

    public void DestroyPlayer(Entity entity)
    {
        monstersTeam.Remove(entity);
        Destroy(entity.gameObject);
    }
    public void SavePlayer(GameSave currentGame)
    {
        Entity entity = Engine.Instance.GetPlayerEntity();

        currentGame.monstersTeam = new List<EntitySaved>();
        foreach (Entity monster in monstersTeam)
        {
            monster.SetPosition(entity.x, entity.y);

            EntitySaved smonster = monster.GetStruct();
            currentGame.monstersTeam.Add(smonster);
        }
    }
    public void SwapPlayer()
    {
        List<EntitySaved> monstersTeam2 = SaveLoad.currentSave.currentGame.monstersTeam;

        Entity preventity = Engine.Instance.GetPlayerEntity();

        preventity.gameObject.SetActive(false);
        SaveLoad.currentSave.currentGame.swapNumber = (SaveLoad.currentSave.currentGame.swapNumber + 1) % monstersTeam2.Count;
        int swap = SaveLoad.currentSave.currentGame.swapNumber;

        Engine.Instance.SetPlayerEntity(monstersTeam[swap]);
        Entity nextentity = Engine.Instance.GetPlayerEntity();
        nextentity.spriteAnimator.SetDirection(preventity.spriteAnimator.currentDirection);
        nextentity.gameObject.SetActive(true);
        nextentity.SetPosition(preventity.x, preventity.y);
       
    }


    public void PlacePlayer()
    {

        if (Engine.Instance.GetPlayerEntity() == null)
        {
            int swap = SaveLoad.currentSave.currentGame.swapNumber;
            List<EntitySaved> monstersTeam2 = SaveLoad.currentSave.currentGame.monstersTeam;
            int c = 0;
            foreach (EntitySaved monster in monstersTeam2)
            {
                Entity mon = Engine.CreateEntity(Engine.Instance.prefabPlayer, true, monster);
                monstersTeam.Add(mon);
                if (c != swap)
                {
                    mon.gameObject.SetActive(false);
                }
                c++;

                mon.gameObject.transform.parent = teamsParent;

            }
            Engine.Instance.SetPlayerEntity(monstersTeam[swap]);
        }
        if (!SaveLoad.currentSave.currentGame.firstLoad)
        {
            Vector2 vector2 = Engine.Instance.gridGenerator.GetEmptySpace(true);
            Engine.Instance.GetPlayerEntity().SetValues((int)vector2.x, (int)vector2.y, null);
        }

    }
}
