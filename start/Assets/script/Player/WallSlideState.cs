using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : PlayerState
{
    public WallSlideState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected() == false)
            stateMachine.ChangeState(player.airState);

        if(yInput>=0)
        rrb.velocity = new Vector2(0, rrb.velocity.y * 0.5f);
        if (Input.GetKeyDown(KeyCode.K))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }
        if (xInput!=0&& player.facingDir != xInput)
                stateMachine.ChangeState(player.idleState);
            
            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    player.facingDir *= -1;
        //    stateMachine.ChangeState(player.dashState);
        //}
    }
}
