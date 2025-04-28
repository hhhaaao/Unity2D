using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private EnemySkeleton skeleton;
    protected Transform player;
    private int moveDir;
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        skeleton = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //player = GameObject.Find("Player").transform;
        player = PlayerManager.instance.player.transform;
        skeleton.anime.speed = 2;
    }
    public override void Update()
    {
        base.Update();
        if (PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth <= 0)
            stateMachine.ChangeState(skeleton.idleState);

        if (player.position.x > skeleton.transform.position.x)
            moveDir = 1;
        else 
            moveDir = -1;
        
        if (skeleton.IsPlayerDetected()&& PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth > 0)
        {
            stateTimer = skeleton.battleTime;//³ðºÞÊ±¼ä
            if (skeleton.IsPlayerDetected().distance < skeleton.attackDis)
                if (canAttack()) {
                    stateMachine.ChangeState(skeleton.attackState);
      
                }

        }
        else
        {
            if (stateTimer < 0||skeleton.IsPlayerDetected().distance>8)
                stateMachine.ChangeState(skeleton.idleState);
        }
        skeleton.SetVelocity(2*skeleton.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        skeleton.anime.speed = 1;
        base.Exit();
    }

    public bool canAttack()
    {
        if (Time.time > skeleton.lastTimeAttack + skeleton.attackCoolDown)
        {
            //skeleton.lastTimeAttack = Time.time;
            return true;

        }
        else
        {
            //Debug.Log("can't attack");
            return false; }
    }
}
