using Assets.Scripts.Datas.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    public bool IsPlayerTurn { get; set; } // Property

    public void EndPlayerTurn()
    {
        IsPlayerTurn = false;
    }


    /* InputHandler may be a limited name, as at the moment deals with Player logic as well */

    PlayerAI playerAI;
    public PlayerInputManager inputManager;

    public static bool isFOVrecompute; // If True, FOV is recomputed on GridGenerator.cs (temporary, maybe this needs to go into GameMap or something)
    //public static MessageLog MessageLog { get; private set; }


    private Entity player;
    private float timeTillNextFrame;

    public void Init()
    {

        UpdatePlayer();
        inputManager = this.GetComponent<PlayerInputManager>();
        isFOVrecompute = true; // When the player appears for first time, we need to calculate the initial FOV
        timeTillNextFrame = 0f;
        inputManager.Init();
    }

    public void UpdatePlayer()
    {
        player = Engine.Instance.GetPlayerEntity();
        playerAI = player.GetComponent<PlayerAI>();
    }

    // removed for be smooth movement
    //private void Resetcoldown()
    //{
    //    timeTillNextFrame = 0.6f / inputManager.speed;
    //}

    private void Update()
    {


        if (GameManager.GetInstance().IsPaused())
        {
            return;
        }

        if (timeTillNextFrame > 0f)
        {
            timeTillNextFrame -= Time.deltaTime;
        }
        else
        {
            if (playerAI == null)
            {
                return;
            }
            //if (movement == null)
            //{
            //    return;
            //}

            Vector2 velocity = inputManager.rawInputMovement;


            IsPlayerTurn = Engine.Instance.gameStateManager.IsPlayerTurn();

            // MOVEMENT
            if (velocity.y>0.5f && playerAI.isPlayerMoving == false && IsPlayerTurn)
            {
                playerAI.MovePlayer(this,GameCharacterDirection.Up);

            }
            else if (velocity.y < -0.5f && playerAI.isPlayerMoving == false && IsPlayerTurn)
            {
                playerAI.MovePlayer(this, GameCharacterDirection.Down);

            }
            else if (velocity.x > 0.5f && playerAI.isPlayerMoving == false && IsPlayerTurn)
            {
                playerAI.MovePlayer(this, GameCharacterDirection.Right);
            }
            else if (velocity.x < -0.5f && playerAI.isPlayerMoving == false && IsPlayerTurn)
            {
                playerAI.MovePlayer(this, GameCharacterDirection.Left);

            }
            else if (inputManager.isWaiting && playerAI.isPlayerMoving == false && IsPlayerTurn)
            {
                inputManager.isWaiting = false;
                EndOfPlayerTurn();
            }

            //else if (Input.GetKey(KeyCode.Z) && playerAI.isPlayerMoving == false && IsPlayerTurn)
            //{
            //    // Give +1Health for waiting
            //    entity.WaitAndRegenerate();
            //    EndOfPlayerTurn();
            //}
        }




        //if (Input.GetKeyDown(KeyCode.Less) && playerAI.isPlayerOnTopOfExitTile == true )
        //{
        //    Debug.Log("Discovering new depths");
        //    Engine.Instance.gridGenerator.NextFloor();

            //    GridGenerator.__generateNextFloor = true; // Switches the generateNewFloor to True and the rest is dealt within the Grid Generator.
            //}

    }
  
    public void UseItem(int _numericKeycode) {
        EndOfPlayerTurn(); //We need to end the turn if the player decides to use an item
    }



    // After a movement, or other player action, we end the turn
    public void EndOfPlayerTurn()
    {
      //  Resetcoldown();
        isFOVrecompute = true; // When a movement is success, then we recompute FOV
        playerAI.isPlayerMoving = false;
        IsPlayerTurn = false;

        GameStateManager.__gameTimeTicks++; // Adds a tick to Game Time.
        Engine.Instance.gameStateManager.SetGameState(GameStateManager.GameState.enemyTurn);
    }


    public void ActivateEnemies()
    {
        IsPlayerTurn = true;
        Debug.Log("Player turn!");

    }



}
