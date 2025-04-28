using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    EnemySkeleton skeleton;
    public SkeletonStunState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        skeleton = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        skeleton.fx.InvokeRepeating("RedColorBlink", 0, .1f);
        stateTimer = 1;
        //skeleton.SetVelocity(-skeleton.facingDir * skeleton.stunDir.x, skeleton.stunDir.y);//◊ÛΩ≈”“Ω≈∑…ÃÏ
        rb.velocity = new Vector2(-skeleton.facingDir * skeleton.stunDir.x, skeleton.stunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
        {
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}
