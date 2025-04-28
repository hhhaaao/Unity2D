using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastState : EnemyState
{
    private EnemyBoss boss;
    private int amountOfSpells;
    private float spellCooldown;
    private float spellTimer;
    public BossCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        boss = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        amountOfSpells=boss.spellsAmount;
        spellTimer = spellCooldown + .5f;
    }

    public override void Update()
    {
        base.Update();

        spellTimer -= Time.deltaTime;

        if (CanCast())
        {
            boss.CastSpell();
        }
        

        if(amountOfSpells<=0)
            stateMachine.ChangeState(boss.teleportState);
    }


    public override void Exit()
    {
        base.Exit();
        boss.lastTimeCast = Time.time;
    }
    private bool CanCast()
    {
        if(amountOfSpells>=0&&spellTimer<0)
        {
            amountOfSpells--;
            spellTimer = boss.spellCooldown;
            return true;
        }
        return false;
    }
}
