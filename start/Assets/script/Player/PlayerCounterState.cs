using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterState : PlayerState
{
    public PlayerCounterState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        triggerCalled = false;//��һ�ε����ɹ�֮��û�ж�����ԭ��
        base.Enter();

        stateTimer = player.counterAttackDuration;
        
        Debug.Log("timer: " + stateTimer);

    }

    public override void Exit()
    {
        player.stats.MakeInvicible(false);
        player.anime.SetBool("ConterSuccess", false);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Debug.Log(animBoolName);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                if(hit.GetComponent<Enemy>().CanBeStunned())//��⵽��ײ�Ҹպ��ڴ򿵴�����
                {
                    stateTimer = 10;//last long
                    player.anime.SetBool("ConterSuccess", true);
                    player.stats.MakeInvicible(true);
                }
        }

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);

    }
}
