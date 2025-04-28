using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public int comboCounter { get;private set; }
    public float lastAttck;
    public float comboWindow=2f;
    public PlayerPrimaryAttackState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        //g
        triggerCalled = false;
        
        base.Enter();
        player.SetVelocity0();
        xInput = 0;//重置xInput
        if (comboCounter > 2||Time.time>lastAttck+comboWindow)
            comboCounter = 0;
        player.anime.SetInteger("ComboCounter", comboCounter);

        #region AttackDir
        float attackDir;
        attackDir=(xInput==0? player.facingDir:xInput);
        
        #endregion
        player.SetVelocity(player.attackMovement[comboCounter] *attackDir, rrb.velocity.y);
        //Debug.Log(comboCounter);
        //player.anime.speed = 3;//动画播放速度

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        //if(triggerCalled)

        //player.StartCoroutine("BusyFor", .15f);//

        player.anime.speed = 1;
        comboCounter++;
        lastAttck = Time.time;
        //Debug.Log(lastAttck);
    }

    public override void Update()
    {
        
        base.Update();
        if (stateTimer < 0)
            //rrb.velocity = new Vector2(0, 0);
            player.SetVelocity0();

        //if(stateTimer<0)
        //{
        //    rrb.velocity = new Vector2(0, 0);
        //}

        //#region test
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //foreach (var hit in colliders)
        //{
        //    if (hit.GetComponent<Enemy>() != null)
        //        if (player.canCounter)//检测到碰撞且两方都刚好在打康窗口内
        //        {
        //            if (hit.GetComponent<Enemy>().CanBeStunned()) {
        //                stateTimer = 10;//last long
        //                player.anime.SetBool("Counter", true);
        //                comboCounter = 0;
        //                //Debug.Log("COUNTER");
        //                 }
        //        }
        //}
        //#endregion

        if (triggerCalled)
        {
            //comboCounter++;
            player.anime.SetBool("Counter", false);//结束打康动画
            stateMachine.ChangeState(player.idleState);
        }
        
    }
}
