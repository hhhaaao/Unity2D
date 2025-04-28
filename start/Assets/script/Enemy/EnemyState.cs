using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected bool triggerCalled;
    protected string animBoolName;
    protected float stateTimer;
    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine,string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        if (enemyBase != null && enemyBase.anime != null)
        {
            enemyBase.anime.SetBool(animBoolName, true);
        }
        else
        {
            Debug.LogError("enemyBase or anime is null in Enter() method");
        }
        //Debug.Log("current state: " + animBoolName);
        rb = enemyBase.rb;
    }


    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        //Debug.Log("time: "+stateTimer);
    }
    public virtual void Exit()
    {
        enemyBase.anime.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimName(animBoolName);//ÕÀ≥ˆ ±…Ë÷√animBoolName
    }

    public virtual void AnimFinTrigger()
    {
        triggerCalled = true;
    }
}
