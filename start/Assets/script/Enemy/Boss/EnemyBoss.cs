using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    #region
    public  BossIdleState idleState { get; private set; }
    public BossBattleState battleState { get; private set; }
    public BossCastState castState { get; private set; }
    public BossAttackState attackState { get; private set; }    
    public BossDeathState deathState { get;private set; }
    public BossTeleportState teleportState { get; private set; }

    #endregion

    public bool bossFightBegins;

    [Header("Teleport Details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport=25;

    [Header("Spell Cast Details")]
    [SerializeField] private GameObject spellPrefab;
    public int spellsAmount;
    public float spellCooldown;

    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;


    protected override void Awake()
    {
        base.Awake();

        facingDir = -1;
        idleState = new BossIdleState(this, stateMachine, "Idle", this);
        battleState = new BossBattleState(this, stateMachine, "Move", this);
        castState = new BossCastState(this, stateMachine, "Cast", this);
        attackState = new BossAttackState(this, stateMachine, "Attack", this);
        deathState = new BossDeathState(this, stateMachine, "Idle", this);
        teleportState = new BossTeleportState(this, stateMachine, "Teleport", this);


    }

    protected override void Start()
    {
        base.Start();
      
         stateMachine.Initialize(idleState);
 
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }

    //´«ËÍ
    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x+3, arena.bounds.max.x-3);
        float y=Random.Range(arena.bounds.min.y+3,arena.bounds.max.y-3);

        transform.position=new Vector3(x,y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if(!GroundBelow()||SomethingIsAround())
        {
            Debug.Log("Look for new position");
            FindPosition();
        }

    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround); 
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero,0, whatIsGround);


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position,surroundingCheckSize);
    }

     public bool CanTeleport()
    {
        if (Random.Range(0, 100) < chanceToTeleport)
        {
            chanceToTeleport=defaultChanceToTeleport;
            return true;
        }
        else
            return false;
    }

    public bool CanCast()
    {
        if (Time.time >= lastTimeCast + spellStateCooldown)
        {
            
            return true;
        }
        else
            return false;
    }

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;
        Vector3 spellPosition = new Vector3(player.transform.position.x+player.facingDir*3,player.transform.position.y+1);

        if (player.rb.velocity.x == 0)
            spellPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1);
        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<CastSpellController>().SetupSpell(stats);
    }
}

