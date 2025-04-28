using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        
        base.Enter();
        skeleton.SetVelocity0();
        stateTimer = skeleton.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("the animName is " + animBoolName);
        if (stateTimer < 0) 
            stateMachine.ChangeState(skeleton.moveState);
        if (skeleton.IsPlayerDetected()&& PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth > 0)
            stateMachine.ChangeState(skeleton.battleState);
    }
}
