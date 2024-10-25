using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool enemyCanMove;
    private bool isEnemyTimeToDoOneAction;

    private Vector3 _lastKnownEnemyPosition;

    private Entity entity;

    //public bool isEnemyAttacked;

    Entity.EntityMode _currentEntityMode;

    void Start()
    {
        entity = GetComponent<Entity>();
        isEnemyTimeToDoOneAction = false; // by default, enemy is idle.
        _currentEntityMode = Entity.EntityMode.Wander;


    }

    public void PlayTurn()
    {
        if (isEnemyTimeToDoOneAction == false)
        {
            _lastKnownEnemyPosition = transform.localPosition;
            isEnemyTimeToDoOneAction = true; // We switch this to true so stops after just this one
            GameObject newenemypost = this.gameObject;
            CheckCurrentState(newenemypost); // We change EnemyMoves() for CheckCurrentState()


        }
    }

    // If is enemy turn, check their current state to decide which action will be next:
    void CheckCurrentState(GameObject _newenemypost)
    {
        // Default mode = wander
        Entity playerReference = Engine.Instance.GetPlayerEntity();
        // If player is within reach: Entity.Alerted -> IsEntityAlerted()
        bool isEnemyAlerted = IsEntityAlerted();
        bool isEnemyAttacked = IsEnemyAtacked();
        //isEnemyAttacked = IsEnemyAtacked();

        if (isEnemyAlerted && isEnemyAttacked)
        {
            // If enemy is attacked, will attack back.
            entity.ResolveAttack( playerReference, Entity.EntityMode.CombatEngaged);
        }
        else if (isEnemyAlerted)
        {
            entity.Alert( Entity.EntityMode.Alerted);
        }
        else
        {
            // if is not, Entity.Wander
            entity.Move( Entity.EntityMode.Wander, 1.0f, 0.0f);
        }

        isEnemyTimeToDoOneAction = false; // We switch this to false so they can move again once is their turn
    }
    bool IsEnemyAtacked() {

        if (entity.isAgressive == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    bool IsEntityAlerted()
    {
        Entity playerReference = Engine.Instance.GetPlayerEntity();
        List<Vector2> _entityVisibilityArea = new List<Vector2>();
        _entityVisibilityArea.Clear(); // Reset for safely from prvious iterations
        Vector2 _playerCoordinates = new Vector2(Mathf.Floor(playerReference.x), Mathf.Floor(playerReference.y));
        int _entityVisibilityRadius = 5; // 
                
        int _offsetQuadrant4 = entity.x - _entityVisibilityRadius;
        int _offsetQuadrant1 = entity.y - _entityVisibilityRadius;
                                                                                        
        for (int x = _offsetQuadrant4; x < entity.x + _entityVisibilityRadius; x++) // from x-5 to x+5
        {
            for (int y = _offsetQuadrant1; y < entity.y + _entityVisibilityRadius; y++) // from y-5 to y+5
            {
                 _entityVisibilityArea.Add(new Vector2(x, y));
            }
        }


        if (_entityVisibilityArea.Contains(_playerCoordinates)) 
        {
            //Debug.Log("Enemy alerted");
            return true;
        }
        else 
        {
            //Debug.Log("Enemy NOT alerted");
            return false; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Enemy")
        {
            entity.SetPosition((int)_lastKnownEnemyPosition.x, (int)_lastKnownEnemyPosition.y);
        }
    }
}
