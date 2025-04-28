using System.Collections;
using UnityEngine;

public class Player : Entity
{
    #region attack detail
    [Header("Attack Details")]
    public float[] attackMovement;//
    public float counterAttackDuration = .2f;
    public bool canCounter;
    #endregion

    #region Move Info
    [Header("Move Info")]
    public float moveSpeed = 12;
    public bool isBusy { get; private set; }
    public float jumpForce = 5;
    public float swordCatchImpact;
    private float defaultMoveSpeed;
    private float defaultJumpForce;
    #endregion
    #region Dash Info
    [Header("Dash Info")]
    public float dashSpeed = 20;
    public float dashDuration = 0.2f;
    private float defaultDashSpeed;

    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashTimer;
    public float dashDir { get; private set; }
    #endregion

    #region Skill
    public SkillManager skill { get; private set; }//

    public GameObject sword { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public WallSlideState wallState { get; private set; }
    public PlayerWallJump wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterState counterState { get; private set; }
    public PlayerAimState aimState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallState = new WallSlideState(stateMachine, this, "Wall");
        wallJump = new PlayerWallJump(stateMachine, this, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(stateMachine, this, "Attack");
        counterState = new PlayerCounterState(stateMachine, this, "CounterAttack");
        aimState = new PlayerAimState(stateMachine, this, "SwordAim");
        catchSwordState = new PlayerCatchSwordState(stateMachine, this, "SwordCatch");
        deadState = new PlayerDeadState(stateMachine, this, "Dead");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        skill = SkillManager.instance;

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;

    }

    protected override void Update()
    {
        if (Time.timeScale == 0)
            return;

        base.Update();
        stateMachine.currentState.Update();

        DashInputCheck();
        isWall = IsWallDetected();
        isGround = IsGroundDetected();

        //ÓÃÒ©Æ¿
        if (Input.GetKeyDown(KeyCode.O))
            Inventory.instance.UseFlask();

    }

    public override void SlowEntityBy(float _slowPercent, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercent);
        jumpForce = jumpForce * (1 - _slowPercent);
        dashSpeed = dashSpeed * (1 - _slowPercent);
        anime.speed *= (1 - _slowPercent);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        dashSpeed = defaultDashSpeed;
        jumpForce = defaultJumpForce;
    }
    public void DashInputCheck()
    {
        //dashTimer-=Time.deltaTime;

        if (SkillManager.instance.dashSkill.dashUnlocked == false)
            return;

        if (Input.GetKeyDown(KeyCode.L) &&/*dashTimer<0*/SkillManager.instance.dashSkill.CanUseSkill())
        {
            //dashTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }

    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        Debug.Log("now is busy");
        yield return new WaitForSeconds(seconds);
        isBusy = false;
        Debug.Log("now is free");
    }

    #region CounterTest
    public virtual void OpenPCounterWindow()
    {
        canCounter = true;
        //counterImage.SetActive(true);
    }

    public virtual void CloseCounterWindow()
    {
        canCounter = false;
        //counterImage.SetActive(false);
    }
    public virtual bool CanCounter()
    {
        if (canCounter)
        {

            CloseCounterWindow();
            return true;
        }
        else
            return false;
    }
    #endregion

    //Îªplayer·ÖÅäsword
    public void AssignNewSword(GameObject newSword)
    {
        sword = newSword;
    }

    //É¾³ýsword
    public void CatchSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

    protected override void SetupZeroKnockbackPower()
    {
        knockBackPower = new Vector2(0, 0);
    }
}

