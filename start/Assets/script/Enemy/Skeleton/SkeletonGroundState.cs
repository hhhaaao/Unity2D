using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected EnemySkeleton skeleton;
    protected Transform player;
    public SkeletonGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        skeleton = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //player = GameObject.Find("Player").transform;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if ((skeleton.IsPlayerDetected() || Vector2.Distance(skeleton.transform.position, player.transform.position) < 2)&& PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth > 0)
        {
           
            stateMachine.ChangeState(skeleton.battleState);
        }

    }
}
