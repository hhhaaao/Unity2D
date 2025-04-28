using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump :PlayerState
{
    public PlayerWallJump(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.4f;
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rrb.velocity.y);
        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);
    }
}
