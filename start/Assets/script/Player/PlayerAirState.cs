using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();
        if(player.IsGroundDetected())
        {
            player.stateMachine.ChangeState(player.idleState);
        }


        if (player.IsWallDetected())
            player.stateMachine.ChangeState(player.wallState);
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rrb.velocity.y);
    }


}
