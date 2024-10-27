using Assets.Scripts.Datas.Enum;
using Assets.Scripts.Datas.Members;
using Assets.Scripts.Datas.Save;
using Assets.Scripts.Datas.Struct;
using UnityEngine;


// inherits from Monobehavior so we can use Destroy()
// inherits from ISchedulable so can be added to our scheduling system
public class Entity : MonoBehaviour
{
    public int x;
    public int y;
    public string entityname;
    public SpriteAnimator spriteAnimator;

    public MonsterStats monsterStats;
    
    public bool isBlockingEntity; // This will differentiate if is a physic body or we can pass through (potion), without need to check colliders or is trigger.
    //public int health;

    public bool isAgressive = false;

    // If sentient ... possibly needs a new class inherits from this one Sentient : Entity , and we add Move() there.
    public enum EntityMode
    {
        Wander, // Done
        Hunt, // TODO // A*
        Sleep, // TODO Change to start the game as Sleep or Wander, wakes up if player is near.
        Alerted, // Done . If player within FOV, enemy changes from wander to alerted .
        CombatEngaged // TODO: WIP combat started. Once receive or do damage.
    }

    private EntityMode _entity;

    // Entity constructor
    public void SetValues(int aX, int aY, string aName ) {

        x = aX;
        y = aY;
        if (aName != null)
        {
            entityname = aName;
        }

        transform.localPosition = new Vector3(aX, aY,0);
    }
    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
        this.transform.localPosition = new Vector3(x, y, 0);
    }


    public void InitMonster(bool isPlayer, EntitySaved tama)
    {
     //   Debug.Log(tama.monster)
        spriteAnimator.Init(tama.monster.monsterSoul.specie.ToString());

        if (isPlayer)
        {
            this.gameObject.AddComponent<PlayerAI>();
        }
        else
        {
            this.gameObject.AddComponent<EnemyAI>();
            this.gameObject.AddComponent<H_Stealth>();
        }
        monsterStats = tama.monster;
        
        SetValues(tama.x, tama.y, tama.monster.monsterSoul.specie.ToString());
    }
    public EntitySaved GetStruct()
    {
        return new EntitySaved()
        {
            monster = monsterStats,
            x = x,
            y = y,
            entityname = entityname,
        };
    }


    public void InitMonster(bool isPlayer,string tama)
    {
        if (isPlayer)
        {
            spriteAnimator.Init(tama);
            this.gameObject.AddComponent<PlayerAI>();
            monsterStats = new MonsterStats(isPlayer, tama);
            this.gameObject.name = "Player_"+tama;

        }
        else
        {
            spriteAnimator.Init(tama);
            this.gameObject.AddComponent<EnemyAI>();
            this.gameObject.AddComponent<H_Stealth>();
            monsterStats = new MonsterStats(isPlayer, tama);
            this.gameObject.name = "Enemi_"+tama;
            //this.gameObject.

        }
    }




    void Move(int dx, int dy) { 

        // TODO: Enemy movement logic
    }

    // TODO MOVE THESE TO FIGHTER.CS
    public void Attack(Entity attacker, Entity defender) {
    }

    /* Used mainly when PLAYER attacks ENEMIES */
    public bool ResolveDefense(Entity defender)
    {
        if (!defender.gameObject.activeSelf)
        {
            return true;
        }
        int damage = (int)monsterStats.GetBaseAttackPower();
        MessageLogManager.Instance.AddToQueue(entityname + ": Atk for <color=green>"+ damage + "</color> hp!");
        //Debug.Log("## COMBAT: Attack successful for (1) hit point!"); // TODO this number will be dynamic
        defender.monsterStats.HP-= damage;
        defender.isAgressive = true;
        //if (_resolved == 0) // Will be because at the moment both are 10
        //{
        //    float _rand = (int)Random.Range(0, 100);
        //    if (_rand < 50)
        //    {
        //        MessageLogManager.Instance.AddToQueue(defender.name + ": Atk blocked!");
        //        //Debug.Log("## COMBAT: Attack blocked!");
        //    }
        //    else
        //    {
        //        MessageLogManager.Instance.AddToQueue(gameObject.name + ": Atk for <color=green>1</color> hp!");
        //        //Debug.Log("## COMBAT: Attack successful for (1) hit point!"); // TODO this number will be dynamic
        //        defender.monsterStats.HP--;
        //        defender.isAgressive = true;
        //    }
        //}

        return ResolveDeath(defender); // Check if somebody is death after a successful attack
    }

    /* Used mainly when ENEMIES attack the PLAYER */
    public bool ResolveAttack(Entity defender, EntityMode _entityMode) {
        // TODO: Ranged attacks works nicely and the player is hit back, but there's no graphical cue of this YET
        MessageLogManager.Instance.AddToQueue(entityname + " Counter Atk back!");
        //ShootEffect(attacker, defender);
        //Debug.Log("## COMBAT: Entity attacks back!!");
        return ResolveDefense( defender);

    }

    public void WaitAndRegenerate() {

        if (monsterStats.HP < 10)
        {
            monsterStats.HP += 1;
        }
    }


    public bool ResolveDeath(Entity defender)
    {
        if (defender.gameObject.activeSelf)
        {
          //  Debug.Log("ResolveDeath:" + defender.monsterStats.HP);
            if (defender.monsterStats.HP <= 0)
            {
                if (defender.tag == "Player")
                {
                    SaveLoad.currentSave.Lose();
                    GameManager.GetInstance().LoadLevel(SceneGame.Dungeon, SceneGame.Start);
                }
                else
                {
                    MessageLogManager.Instance.AddToQueue(defender.entityname + " has been destroyed");
                    Engine.Instance.levelSystem.AddXP(monsterStats);
                    Engine.Instance.enemyGenerator.DestroyEnemy(defender);
                }
                return true;
            }
        }
        return false;
    }

    public void Move(EntityMode _entityMode, float dx, float dy)
    {

        Color _wanderColor = new Color32(255, 255, 255, 255);
        spriteAnimator.spriteRenderer.color = _wanderColor;
        // TODO: refactor
        // dx and dy can be speed, in this case they move 1

        float _rand = (int)Random.Range(0, 3.99f);
        //Q: Does this gets a new _rand for each entity?  A: Yes.
        //Debug.Log(entityThatMoves.name + " moves " + _rand); 
        //Q: Once assigned, they always take the same direction for all moves? A: No. 
        if (_entityMode == EntityMode.Wander)
        {

            switch (_rand)
            {
                case 0:
                    SetPosition(x, y + 1);
                    break;
                case 1:
                    SetPosition(x, y - 1);

                    break;
                case 2:
                    SetPosition(x - 1, y);
                    break;
                case 3://right x++ y=0
                    //_dx += 1.0f;
                    SetPosition(x + 1, y);
                    break;
            }
        }
    }


    public void Alert( EntityMode _entityMode) {
        // Entity.Alert(_newenemypost, Entity.EntityMode.Alerted);
        Color _alertColor = new Color32(226,44,10, 255);
        //MessageLogManager.Instance.AddToQueue(_entity.name + " has been alerted.");
        // TESTING WIP Switch sprite
        spriteAnimator.spriteRenderer.color = _alertColor;
    }

    public void ResolveItem(GameObject _item)
    {
        Engine.Instance.itemGenerator.ResolveItem(_item);
    }

    public void PlayerDeath(GameObject player)
    {

        player.SetActive(false);
        Debug.Log("You die!");
    }


    void AnalizeMapAroundEntity()
    {

    }

}
