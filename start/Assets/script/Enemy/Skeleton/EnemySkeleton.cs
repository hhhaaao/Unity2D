using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region state
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunState stunState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();  
        idleState = new SkeletonIdleState(this, stateMachine, "Idle",this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunState = new SkeletonStunState(this, stateMachine, "Stun", this);
        deadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        //Debug.Log("rb is: " + rb);  // 检查 Rigidbody2D 是否正确赋值
        //Debug.Log("anime is: " + anime);  // 检查 Animator 是否正确赋值

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        {
            //if(Input.GetKeyDown(KeyCode.H))
            //    stateMachine.ChangeState(stunState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        { stateMachine.ChangeState(stunState);
        return true; }
        else return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
 