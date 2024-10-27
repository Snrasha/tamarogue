
using Assets.Scripts.Datas.Enum.Stats;
using Assets.Scripts.Datas.Struct;
using System.Collections.Generic;

namespace Assets.Scripts.Datas.Save
{
    public class DataSave
    {
        public bool loaded;
        public bool gameInProgress;
        public int slot;
        public GameSave currentGame;
        public List<Species> unlockedMonsters;


        

        public DataSave( )
        {
            unlockedMonsters = new List<Species>();
            unlockedMonsters.Add(Species.Taple);
            unlockedMonsters.Add(Species.Dragonee);
            unlockedMonsters.Add(Species.Kobou);
            slot = 1;
            loaded = true;
            gameInProgress = false;
            currentGame = new GameSave();
        }
        public DataSave(SaveStruct saveStruct)
        {
            loaded = false;
            slot = saveStruct.slot;
            currentGame = new GameSave(saveStruct.currentGame);
            gameInProgress = saveStruct.gameInProgress;

            unlockedMonsters = new List<Species>();
            for (int i = 0; i < saveStruct.unlockedMonsters.Length; i++)
            {
                unlockedMonsters.Add(saveStruct.unlockedMonsters[i]);
            }
        }
        public void Lose()
        {
            gameInProgress = false;
            currentGame = new GameSave();
        }
    }


    [System.Serializable]
    public struct Prefs
    {
        public bool loaded;
        public int currentSlot;
        public int effectsVolume;
        public int musicVolume;
        public int totalVolume;
        public int resolutionChoice;
        public int sizeScreenChoice;
        public string languageSelect;

        public string previousScene;
    }

    [System.Serializable]
    public struct SaveStruct
    {
        public int slot;
        public bool gameInProgress;
        public GameStructWrapper currentGame;

        public Species[] unlockedMonsters;

        public SaveStruct(DataSave dataStruct)
        {
            this.slot = dataStruct.slot;
            gameInProgress = dataStruct.gameInProgress;
            currentGame = new GameStructWrapper(dataStruct.currentGame);
            unlockedMonsters = dataStruct.unlockedMonsters.ToArray();

            
        }

    }
    public class GameSave
    {
        public int floor;
        public int swapNumber = 0;
     //   public int hunger; // No Hunger system, just TP regen between each floor.
        public List<EntitySaved> monstersTeam;
        public List<EntitySaved> enemyList;
        public ItemStruct[] itemArray;
        public ExitStruct[] exitArray;

        public ulong seed;
        public bool firstLoad;




        public GameSave()
        {
            this.floor = 0;
            //   this.hunger = 100;
            swapNumber = 0;
               monstersTeam = new List<EntitySaved>();
            monstersTeam.Add(new EntitySaved("Kobou"));
            monstersTeam.Add(new EntitySaved("Taple"));
            itemArray = new ItemStruct[0];
            exitArray = new ExitStruct[0];
            enemyList = new List<EntitySaved>();
            firstLoad = false;

            seed = 0;

        }
        public GameSave(GameStructWrapper gameSave)
        {
            this.floor = gameSave.floor;
        //    this.hunger = gameSave.hunger;
            seed = gameSave.seed;
            itemArray = gameSave.itemArray;
            exitArray = gameSave.exitArray;
            swapNumber= gameSave.swapNumber;


            monstersTeam = new List<EntitySaved>();
            if (gameSave.monstersTeam!=null)
            {
                for (int i = 0; i < gameSave.monstersTeam.Length; i++)
                {
                    EntityStructwrapper item = gameSave.monstersTeam[i];
                    monstersTeam.Add(new EntitySaved(item));
                }
            }
            enemyList = new List<EntitySaved>();
            if (gameSave.enemyList != null)
            {
                for (int i = 0; i < gameSave.enemyList.Length; i++)
                {
                    EntityStructwrapper item = gameSave.enemyList[i];
                    enemyList.Add(new EntitySaved(item));
                }
            }
            firstLoad = monstersTeam.Count > 0;
        }
    }


    [System.Serializable]
    public struct GameStructWrapper
    {
        public int floor;
        public int swapNumber;
    //    public int hunger;
        public EntityStructwrapper[] monstersTeam;
        public EntityStructwrapper[] enemyList;
        public ItemStruct[] itemArray;
        public ExitStruct[] exitArray;

        public ulong seed;
        
        public GameStructWrapper(GameSave gameSave)
        {
            this.floor = gameSave.floor;
       //     this.hunger = gameSave.hunger;
            seed = gameSave.seed;
            itemArray = gameSave.itemArray;
            exitArray = gameSave.exitArray;
            swapNumber = gameSave.swapNumber;
            monstersTeam = new EntityStructwrapper[gameSave.monstersTeam.Count];
            for (int i = 0; i < gameSave.monstersTeam.Count; i++)
            {
                EntitySaved item = gameSave.monstersTeam[i];
                monstersTeam[i] = new EntityStructwrapper(item);
            }
            enemyList = new EntityStructwrapper[gameSave.enemyList.Count];
            for (int i = 0; i < gameSave.enemyList.Count; i++)
            {
                EntitySaved item = gameSave.enemyList[i];
                enemyList[i] = new EntityStructwrapper(item);
            }
        }
    }
}