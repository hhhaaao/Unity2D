using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
        
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("the animName is " + animBoolName);
        skeleton.SetVelocity(skeleton.moveSpeed * skeleton.facingDir, rb.velocity.y);

        if(skeleton.IsWallDetected()||!skeleton.IsGroundDetected())
        {
            skeleton.SetVelocity0();
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.idleState);
        }
        if (skeleton.IsPlayerDetected()&& PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth > 0)
            stateMachine.ChangeState(skeleton.battleState);
    }
}
