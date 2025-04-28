using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    EnemySkeleton skeleton;

    public SkeletonAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        skeleton = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.lastTimeAttack=Time.time;
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity0();
        if (triggerCalled)
            stateMachine.ChangeState(skeleton.battleState);
        if (PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth <= 0)
            stateMachine.ChangeState(skeleton.idleState);


    }
}
