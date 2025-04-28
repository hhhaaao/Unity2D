using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleportState : EnemyState
{
    private EnemyBoss boss;
    public BossTeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        boss = _enemy;
    }


    public override void Enter()
    {
        base.Enter();
        boss.stats.MakeInvicible(true);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (boss.CanCast())
                stateMachine.ChangeState(boss.castState);
            else 
                stateMachine.ChangeState(boss.battleState);
        }




    }


    public override void Exit()
    {
        base.Exit();
        boss.stats.MakeInvicible(false);
    }
}
