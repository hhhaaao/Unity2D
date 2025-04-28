using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDeathState : EnemyState
{
    private EnemyBoss boss;
    public BossDeathState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        boss= _enemy;   
    }

    public override void Enter()
    {
        base.Enter();

        boss.anime.SetBool(boss.lastBoolName, true);
        boss.anime.speed = 0;
        boss.cd.enabled = false;

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 10);
        }
    }
}
