using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Collision Info
    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDis;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDis;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected bool isWall;
    [SerializeField] protected bool isGround;
    #endregion

    #region Attack Check
    [Header("Attack Info")]//
    public Transform attackCheck;
    public float attackCheckRadius;
    #endregion

    public int facingDir = 1;
    public bool facingRight = true;

    #region Components
    public Animator anime { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get;private set; }

    

    #endregion

    [Header("KnockBack Info")]
    [SerializeField] protected Vector2 knockBackPower; 
    [SerializeField] protected float knockBackDuration;
    protected bool isKnocked;

    public int knockBackDir { get; private set; }



    public System.Action onFlipped;

    protected virtual void Awake()
    {

    }
    
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponentInChildren<Animator>();
        fx = GetComponentInChildren<EntityFX>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }
   
    protected virtual void Update()
    {

    }

    #region collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDis, whatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDis));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDis, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    public virtual void SlowEntityBy(float _slowPercent,float _slowDuration)
    {

    }

    public virtual void SetupKnockBackDir(Transform _damage)
    {
        if(_damage.position.x>transform.position.x)
        {
            knockBackDir = -1;
        }
        else if(_damage.position.x<transform.position.x)
        {
            knockBackDir = 1;
        }
    }


    protected virtual void ReturnDefaultSpeed()
    {
        anime.speed = 1;
    }
    #region Flip
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(onFlipped!=null)
         onFlipped();

    }

    public void FlipController(float x)
    {
        if (x > 0 && !facingRight)
            Flip();
        else if (x < 0 && facingRight)
            Flip();
    }
    #endregion
    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");//闪烁
        StartCoroutine("HitKnockBack");
        //Debug.Log(gameObject.name + " is damaged");
    }

    public void SetupKnockbackPower(Vector2 _knockBackPower) => knockBackPower = _knockBackPower;
    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockBackPower.x * knockBackDir, knockBackPower.y);
        yield return new WaitForSeconds(knockBackDuration);
        isKnocked = false;
        SetupZeroKnockbackPower();

    }

    #region velocity
    public void SetVelocity(float Vx, float Vy)
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(Vx, Vy);
        FlipController(Vx);
    }

    public void SetVelocity0() /*=> rb.velocity = new Vector2(0, 0);*/
    //代码只有一行
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(0, 0); 
}
    #endregion

    public virtual void Die()
    {

    }

    protected virtual void SetupZeroKnockbackPower()
    {
       
    }
}
