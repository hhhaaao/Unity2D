using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
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
        player.SetVelocity0();
        if (Input.GetKeyUp(KeyCode.U)||Input.GetKeyUp(KeyCode.Mouse1))
            player.stateMachine.ChangeState(player.idleState);
        player.skill.swordSkill.DotsActive(true);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Ãé×¼×ªÏò
        if (player.transform.position.x > mousePosition.x && player.facingRight)
            player.Flip();
        else if (player.transform.position.x < mousePosition.x && !player.facingRight)
            player.Flip();
    }
}
