using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    

    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
      if(SkillManager.instance.cloneSkill.isUnlocked==true)
       player.skill.cloneSkill.CreateClone(player.transform);

        player.stats.MakeInvicible(true);

        
    }  

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rrb.velocity.y);
        player.stats.MakeInvicible(false);
    }

    public override void Update()
    {
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);
        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallState);

        player.fx.CreateAfterImage();
    }

  
}
