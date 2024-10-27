using Assets.Scripts.Datas.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConfirmationPopupStandard;

public class PlayerAI : MonoBehaviour,ConfirmListener
{
    public bool isPlayerOnTopOfExitTile;
    public bool isPlayerMoving;
    public Vector3 _lastKnownPlayerPosition; // We use this to track players last position before their next move
    public Vector3Int _NextPlayerPosition; // We use this to track players last position before their next move

    private Entity player;

    void Awake()
    {
        isPlayerOnTopOfExitTile = false;
        isPlayerMoving = false;
        player = GetComponent<Entity>();
        CreateFOVCollisionArea();

    }

    void CreateFOVCollisionArea()
    {
        //// PROBLEM: Whatever I do or how I create this, moves the player to 0 0 0 . Resolved by adding a rigidbody2d
        //GameObject FOVCollisionHolder = Resources.Load<GameObject>("Prefabs/_FOVCollisionHolder");
        //GameObject _FOVCollisionHolder = Instantiate(FOVCollisionHolder, gameObject.transform.position, Quaternion.identity);
        //_FOVCollisionHolder.transform.SetParent(gameObject.transform); // Adds it to the Player
        //_FOVCollisionHolder.transform.localPosition = new Vector3(0, 0, 0); // Resets it to the center to the player parent

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "Exit")
        {
            isPlayerOnTopOfExitTile = true;
            ConfirmationPopupStandard.instance.OpenPopup("Go down ?",this);

         //   entity.ResolveItem(collision.gameObject);
        }
        if (collision.tag == "Item")
        {
            player.ResolveItem(collision.gameObject);
        }

    }

    //// Exit logic. This must be this way because the collisions is called on FixedUpdate, which may not coincide with your key press and not trigger, so we have a bool switch and check for changes in Update() instead.
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Exit")
    //    {
    //        isPlayerOnTopOfExitTile = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Exit")
    //    {
    //        isPlayerOnTopOfExitTile = false;
    //    }
    //}
    public void MovePlayer(InputHandler inputHandler,GameCharacterDirection direction)
    {
        player.spriteAnimator.SetDirection(direction);
        if (inputHandler.inputManager.isRotating)
        {
            return;
        }
        isPlayerMoving = true;
        _lastKnownPlayerPosition =transform.localPosition;

        switch (direction)
        {
            case GameCharacterDirection.Up:
                _NextPlayerPosition = new Vector3Int(player.x, player.y + 1, 0);
                break;
            case GameCharacterDirection.Down:
                _NextPlayerPosition = new Vector3Int(player.x, (int)player.y - 1, 0);
                break;
            case GameCharacterDirection.Right:
                _NextPlayerPosition = new Vector3Int((int)player.x + 1, player.y, 0);
                break;
            case GameCharacterDirection.Left:
                _NextPlayerPosition = new Vector3Int((int)player.x - 1, player.y, 0);
                break;
            default:
                Debug.Log("There's a wall there, not walkable.");
                break;
        }

        


        if (!Engine.Instance.OnGround(_NextPlayerPosition.x, _NextPlayerPosition.y))
        {
          //  transform.localPosition = new Vector3(_lastKnownPlayerPosition.x, _lastKnownPlayerPosition.y, 0);
            isPlayerMoving = false;
        }
        else
        {
            Entity enemy = Engine.Instance.enemyGenerator.HasEnemy(_NextPlayerPosition.x, _NextPlayerPosition.y);
            if (enemy)
            {

                StartCoroutine(SmoothAttack(inputHandler, _NextPlayerPosition, enemy));

            }
            else
            {
                StartCoroutine(SmoothMovement(inputHandler, _NextPlayerPosition));
            }
        }
    }
    private IEnumerator SmoothAttack(InputHandler inputHandler, Vector3Int destination, Entity enemy)
    {

        GameManager.GetInstance().isPausedMenu = true;
        //      GetComponent<Entity>().spriteAnimator.SetAnim(SpriteAnimator.AnimationEnum.run);
        while (transform.localPosition != destination)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, inputHandler.inputManager.speed * 4 * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Bump attack: resolving defense!");

        bool enemydeath = player.ResolveDefense(enemy);
        // Debug.Log("enemydeath:" + enemydeath);


        while (transform.localPosition != _lastKnownPlayerPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _lastKnownPlayerPosition, inputHandler.inputManager.speed * 2 * Time.deltaTime);
            yield return null;
        }



        //    GetComponent<Entity>().spriteAnimator.SetAnim(SpriteAnimator.AnimationEnum.idle);


        GameManager.GetInstance().isPausedMenu = false;

        inputHandler.EndOfPlayerTurn();

    }

    private IEnumerator SmoothMovement(InputHandler inputHandler,Vector3Int destination)
    {
        GameManager.GetInstance().isPausedMenu = true;

        player.spriteAnimator.SetAnim(SpriteAnimator.AnimationEnum.run);
        // Debug.Log(destination + " " + move) ;
        while (transform.localPosition != destination)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, inputHandler.inputManager.speed * Time.deltaTime);
            yield return null;
            //  
            //   yield return new WaitForEndOfFrame();
        }
        player.SetPosition(destination.x, destination.y);

        player.spriteAnimator.SetAnim(SpriteAnimator.AnimationEnum.idle);

        Engine.Instance.minimap.UpdateMap(Engine.Instance.gridGenerator.map, player.x, player.y);

        GameManager.GetInstance().isPausedMenu = false;

        inputHandler.EndOfPlayerTurn();

    }


    // Exit popup
    public void OnClickConfirmPopupYes()
    {
        Engine.Instance.gridGenerator.NextFloor();
    }

    public void OnClickConfirmPopupNo()
    {
        
    }
}
