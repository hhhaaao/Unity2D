using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyState
{
    private EnemyBoss boss;
    public BossAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        boss = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        boss.chanceToTeleport += 5; 

    }

    public override void Exit()
    {
        base.Exit();
        boss.lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();
        boss.SetVelocity0();
        if (triggerCalled)
        {
            if(boss.CanTeleport())
                stateMachine.ChangeState(boss.teleportState);
            else
                stateMachine.ChangeState(boss.battleState);
        }
       


    }
}
