using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : EnemyState
{
    private EnemyBoss boss;
    private Player player;
    public BossIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        boss= _enemy;
    }

    public override void Enter()
    {

        base.Enter();
        boss.SetVelocity0();
        stateTimer = boss.idleTime;
        player = PlayerManager.instance.player;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("the animName is " + animBoolName);
        //if (stateTimer < 0)
        //    stateMachine.ChangeState(boss.battleState);
        //if (boss.IsPlayerDetected() && PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth > 0)
        //    stateMachine.ChangeState(boss.battleState);
        if (Vector2.Distance(player.transform.position, boss.transform.position) < 7)
        {
            boss.bossFightBegins = true;
            player.fx.ScreenShake(player.fx.bossTriggeredImpact);
        }
        //if (Input.GetKeyDown(KeyCode.V))
        //    stateMachine.ChangeState(boss.teleportState);
        if (stateTimer < 0&&boss.bossFightBegins)
            stateMachine.ChangeState(boss.battleState);
    }
}
