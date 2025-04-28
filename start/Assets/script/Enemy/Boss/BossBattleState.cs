using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleState : EnemyState
{

    private EnemyBoss boss;
    private Transform player;
    private int moveDir;
    public BossBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        boss = _enemy;
    }


    public override void Enter()
    {
        base.Enter();
        //player = GameObject.Find("Player").transform;
        player = PlayerManager.instance.player.transform;
        boss.anime.speed = 2;
    }
    public override void Update()
    {
        base.Update();
        if (PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth <= 0)
            stateMachine.ChangeState(boss.idleState);

        
        if (boss.IsPlayerDetected())
        {
            stateTimer = boss.battleTime;//³ðºÞÊ±¼ä
            
            if (boss.IsPlayerDetected().distance < boss.attackDis)
            {
                if (canAttack())
                {
                    stateMachine.ChangeState(boss.attackState);

                }
                else
                    stateMachine.ChangeState(boss.idleState);
            }
        }
        if (player.position.x > boss.transform.position.x)
            moveDir = 1;
        else
            moveDir = -1;

        if (boss.IsPlayerDetected() && boss.IsPlayerDetected().distance < boss.attackDis - .1f)
            return;

        boss.SetVelocity(2 * boss.moveSpeed * moveDir, rb.velocity.y);

        

    }

    public override void Exit()
    {
        boss.anime.speed = 1;
        base.Exit();
    }

    public bool canAttack()
    {
        if (Time.time > boss.lastTimeAttack + boss.attackCoolDown)
        {
            //boss.lastTimeAttack = Time.time;
            return true;

        }
        else
        {
            //Debug.Log("can't attack");
            return false;
        }
    }
}
