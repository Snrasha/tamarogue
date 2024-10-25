using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<Entity> listOfEnemyEntities = new List<Entity>();

    public Transform enemiesParent;

    public Entity HasEnemy(int x,int y)
    {
        foreach (Entity enemi in listOfEnemyEntities)
        {
            if (enemi.gameObject.activeSelf && enemi.x == x && enemi.y == y)
            {
                return enemi;
            }
        }
        return null;
    }

    public void DestroyEnemy(Entity entity)
    {
        Debug.Log("DestroyEnemy: " + entity.monsterStats.HP+" "+ listOfEnemyEntities.Count);
        listOfEnemyEntities.Remove(entity);
        entity.gameObject.SetActive(false);
        Destroy(entity.gameObject);
        Debug.Log("DestroyEnemy2: " + listOfEnemyEntities.Count);
    }
    public void SaveEnemies(GameSave currentGame)
    {
        Debug.Log("SaveEnemies: " + listOfEnemyEntities.Count);

        currentGame.enemyList = new List<EntitySaved>();
        foreach (Entity enemy in listOfEnemyEntities)
        {
            if (enemy.gameObject.activeSelf)
            {
                EntitySaved savedenemy = enemy.GetStruct();
                currentGame.enemyList.Add(savedenemy);
            }
        }
        Debug.Log("SaveEnemies2: " + listOfEnemyEntities.Count);

    }
    public void Clear()
    {
        foreach (Entity item in listOfEnemyEntities)
        {
            Destroy(item.gameObject);
        }
        listOfEnemyEntities.Clear();
    }

    public void LoadEnemies()
    {
        List<EntitySaved> enemis = SaveLoad.currentSave.currentGame.enemyList;
        foreach (EntitySaved enemy in enemis)
        {
            Entity npcInstance = Engine.CreateEntity(Engine.Instance.prefabEntity, false, enemy);
            listOfEnemyEntities.Add(npcInstance);
            npcInstance.gameObject.transform.parent = enemiesParent;

        }
        return;
    }


    public void PlaceEntities(GridGenerator gridGenerator)
    {
        if (SaveLoad.currentSave.currentGame.firstLoad)
        {
            LoadEnemies();
        }



        int _numberOfEnemyEntities = 10;

        /* Basic implementation of "Increase difficulty as depth progresses" */
        // TODO: Possibly create an array of GO's where I can locate the different enemies and spawn with certain weight based on level.    
        if (SaveLoad.currentSave.currentGame.floor < 5)
        {

        }
        else if (SaveLoad.currentSave.currentGame.floor >= 5)
        {
            _numberOfEnemyEntities = 15;
        }

        // For each entity to be created, find a suitable spawning place and Instantiate an enemy
        for (int i = 0; i < _numberOfEnemyEntities; i++)
        {

            Vector2 _randomVector = gridGenerator.GetEmptySpace();


            Entity npcInstance = Engine.CreateEntity(Engine.Instance.prefabEntity, (int)_randomVector.x, (int)_randomVector.y, "Enemy", new Vector3(_randomVector.x, _randomVector.y, 0));
            npcInstance.InitMonster(false,"Taple");
            listOfEnemyEntities.Add(npcInstance);

            npcInstance.gameObject.transform.parent = enemiesParent;

        }
    }
}
