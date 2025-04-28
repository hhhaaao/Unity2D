using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;

    public PlayerCatchSwordState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword = player.sword.transform;
        //screenShake
        player.fx.ScreenShake(player.fx.shakeSwordImpact);

        //特效
        player.fx.PlayDustFX();
        //接剑转向
        if (player.transform.position.x > sword.position.x && player.facingRight)
            player.Flip();
        else if (player.transform.position.x < sword.position.x && !player.facingRight)
            player.Flip();
        rrb.velocity = new Vector2(-player.facingDir * player.swordCatchImpact, 0);
    }

    public override void Exit()
    {
        triggerCalled = false;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
            player.stateMachine.ChangeState(player.idleState);
    }
}
