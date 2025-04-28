using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerState 

{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected string animBoolName;
    protected Rigidbody2D rrb;

    protected float xInput;
    protected float yInput;

    public float stateTimer;
    protected bool triggerCalled;
    
    public PlayerState(PlayerStateMachine _stateMachine,Player _player,string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }


    public virtual void Enter()
    {
        player.anime.SetBool(animBoolName, true);
        rrb = player.rb;
        
    }

    public virtual void Update()
    {
        //Debug.Log("I'm in " + animBoolName);
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anime.SetFloat("yVelocity", rrb.velocity.y);
        stateTimer-=Time.deltaTime;
    }
    
    public virtual void Exit()
    {
        player.anime.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

}
