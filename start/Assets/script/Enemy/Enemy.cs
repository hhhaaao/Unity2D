using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D) )]
[RequireComponent (typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
[RequireComponent(typeof(ItemDrop))]
public class Enemy : Entity
{
    [SerializeField]protected LayerMask whatIsPlayer;
    [Header("Stun Info")]
    public float stunDuration;
    public Vector2 stunDir;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;//红色感叹号




    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    private float defaultMoveSpeed;

    [Header("Attack Info")]
    public float attackDis;
    public float attackCoolDown;
    [HideInInspector] public float lastTimeAttack;
    public EnemyStateMachine stateMachine { get; private set; }
    public string lastBoolName { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

    }


    
    protected override void Start()
    {
        base.Start();
      

        //Debug.Log("rb is: " + rb);  // 检查 Rigidbody2D 是否正确赋值
        //Debug.Log("anime is: " + anime);  // 检查 Animator 是否正确赋值
        defaultMoveSpeed =moveSpeed;
        

    }
    protected override void Update()
    {
        base.Update();
        //Debug.Log(stateMachine.currentState);
        //Debug.Log("time: "+Time.time);
        stateMachine.currentState.Update();
        //if (IsPlayerDetected())
        //    Debug.Log("player detected");
    }

    
    public virtual RaycastHit2D IsPlayerDetected()
    {
        
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 10, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDis * facingDir, transform.position.y));

        }

    public virtual void AnimeFinishTrigger()=>stateMachine.currentState.AnimFinTrigger();

    #region Stunned
    public virtual void OpenCounterWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterWindow()
    {
        canBeStunned=false;
        counterImage.SetActive(false);
    }
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {

            CloseCounterWindow();
            return true;
        }
        else
            return false;
    }
    #endregion
    
    public virtual void AssignLastAnimName(string animName) => lastBoolName = animName;

    public override void SlowEntityBy(float _slowPercent, float _slowDuration)
    {
        moveSpeed *= (1 - _slowPercent);
        anime.speed *= (1 - _slowPercent);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = defaultMoveSpeed;
    }

    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anime.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anime.speed = 1;

        }
    }

    protected virtual IEnumerator FreezeTimeCoroutine(float _seconds)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_seconds);
        FreezeTime(false);
    }

    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimeCoroutine(_duration));
    
}
