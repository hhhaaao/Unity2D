using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState :PlayerState
{
    public PlayerGroundedState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        //triggerCalled = false;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!player.IsGroundDetected())
            player.stateMachine.ChangeState(player.airState);

        if ((Input.GetKeyDown(KeyCode.U)||Input.GetKeyDown(KeyCode.Mouse1))&&HaveNoSword())
            player.stateMachine.ChangeState(player.aimState);

        if(Input.GetKeyDown(KeyCode.H))
            player.stateMachine.ChangeState(player.counterState);

        if (Input.GetKeyDown(KeyCode.K)&&player.IsGroundDetected())
            player.stateMachine.ChangeState(player.jumpState);
        if (Input.GetKeyDown(KeyCode.J))
            player.stateMachine.ChangeState(player.primaryAttack);


        
    }

    private bool HaveNoSword()
    {
        if (!player.sword)
        {
            return true;

        }
            player.sword.GetComponent<SwordSkillController>().ReturnSword();
            return false;
            
    }
}
