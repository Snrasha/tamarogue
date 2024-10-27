using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager: MonoBehaviour
{
    public enum GameState // possible states
    {
        //noTurns,
        playerTurn,
        enemyTurn
    }
    private GameState __gameState; // current state
    public static int __gameTimeTicks; // Starting with events queue.
    public GameState previousGameState; // 1 - Declare

    public void Init()
    {
     

        // Initial game state is set to playerTurn
        //previousGameState = GameState.noTurns;
        __gameTimeTicks = 0;   // Starts with gameTime = 0
      //  textGameTimeTicks.text = __gameTimeTicks.ToString();
        SetGameState(GameState.playerTurn);

    }

    private void Update()
    {
        // Turns are managed here.
        if (IsEnemyTurn())
        {
            DoEnemyStuff();
            SetGameState(GameState.playerTurn);
        }
    }

    void DoEnemyStuff() {
        foreach(Entity child in Engine.Instance.enemyGenerator.listOfEnemyEntities)
        {
            EnemyAI enemi=child.GetComponent<EnemyAI>();
            if (enemi != null)
            {
                enemi.PlayTurn();
            }
        }

        //Debug.Log("Enemy AI does their stuff. We shouldn't call any action here, but just enemyAI reading the current game State");
        //Entity.TestMove();
    }

    public void SetGameState( GameState newGameState) {

        // Do a check to see if the gamestate is new or do nothing (maybe?) You could have multiple turns if fast enough.

        switch (newGameState)
        {
            case GameState.playerTurn:
                //Debug.Log("Player turn");
                //Debug.Log("Time:" + __gameTimeTicks);
                //__gameState = GameState.enemyTurn;
                break;
            case GameState.enemyTurn:
                //Debug.Log("Enemy turn");
                //StartCoroutine(_temp_enemy_turn());
                // myObject.GetComponent<MyScript>().MyFunction(); for the enemies.
                break;
        }
      //  Engine.Instance.statusManager.UpdateUI();
    }
    public bool IsPlayerTurn()
    {
        return __gameState == GameState.playerTurn;
    }
    public bool IsEnemyTurn()
    {
        return __gameState == GameState.enemyTurn;
    }

}
