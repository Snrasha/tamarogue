using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;
using RogueElements.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO; // files + OS
using System.Runtime.Serialization.Formatters.Binary; // BinaryFormatter access
using UnityEngine;
using UnityEngine.UI;

//using System.Diagnostics;
//using System;

public class Engine : MonoBehaviour
{

    Entity _playerInstance;

    public StatusManager statusManager;
    public GridGenerator gridGenerator;
    public ItemGenerator itemGenerator;
    public EnemyGenerator enemyGenerator;
    public PlayerGenerator playerGenerator;

    public GameStateManager gameStateManager;
    public InputHandler inputHandler;
    public CameraController cameraController;
    public Minimap minimap;

    public GameObject prefabEntity;
    public GameObject prefabPlayer;

    public static Engine Instance;


    public void Start()
    {
        GameManager.GetInstance().isPausedMenu = true;
        Instance = this;
        // Places player in a valid floorTile

        try
        {

            gridGenerator.Init();

            minimap.Init();
            minimap.ResetTiles(gridGenerator.map);

            gameStateManager.Init();

            statusManager.Init();
            inputHandler.Init();
            cameraController.Init();
        }
        catch(Exception e)
        {

        }
        GameManager.GetInstance().isPausedMenu = false;
    }
    public void SetPlayerEntity(Entity player)
    {
        _playerInstance = player;
    }
    public Entity GetPlayerEntity()
    {
        return _playerInstance;
    }
    public bool OnGround(int x,int y)
    {
        return gridGenerator.map.Tiles[x][y].ID == BaseMap.ROOM_TERRAIN_ID;
    }
    public bool HasEnemiesOn(int x,int y)
    {
        return enemyGenerator.HasEnemy(x, y) != null;
    }
    public static Entity CreateEntity(GameObject prefab,bool isPlayer,EntitySaved entitySaved)
    {
        GameObject gameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        Entity entity = gameObject.GetComponent<Entity>();
        entity.InitMonster(isPlayer, entitySaved);
        return entity;
    }
    public static Entity CreateEntity(GameObject prefab,  int aX, int aY, string aName, Vector3 aEntityLocation)
    {
        GameObject gameObject= Instantiate(prefab, aEntityLocation, Quaternion.identity);
        Entity entity = gameObject.GetComponent<Entity>();
        entity.SetValues(aX, aY, aName);
        return entity;
    }
    public void OnDestroy()
    {
        Instance = null;
    }
    public void SaveState()
    {

        //itemGenerator.SaveExits(SaveLoad.currentSave.currentGame);

        GameSave currentGame = SaveLoad.currentSave.currentGame;
        
        //if(currentGame.monstersTeam.Count)
        // currentGame.monstersTeam[0]

        SaveLoad.currentSave.gameInProgress = true;
        playerGenerator.SavePlayer(currentGame);
        enemyGenerator.SaveEnemies(currentGame);
        itemGenerator.SaveItems(currentGame);
        itemGenerator.SaveExits(currentGame);


        SaveLoad.SaveFile();

        //   SaveLoad.currentSave.
    }

}
