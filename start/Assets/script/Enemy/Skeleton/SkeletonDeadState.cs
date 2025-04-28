using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    EnemySkeleton skeleton;
    public SkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        skeleton = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        skeleton.anime.SetBool(skeleton.lastBoolName, true);
        skeleton.anime.speed = 0;
        skeleton.cd.enabled = false;

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer>0)
        {
            rb.velocity = new Vector2(0, 10);
        }
    }
}
