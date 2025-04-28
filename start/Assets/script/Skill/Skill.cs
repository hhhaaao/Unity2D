using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public float coolDown;
    protected float coolDownTimer;
    protected Player player/*=PlayerManager.instance.player*/;
    protected virtual void Start()
    {
        player = PlayerManager.instance.player;

        CheckUnlock();
    }
    protected virtual void Update()
    {
        coolDownTimer-=Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(coolDownTimer<0)
        {
            UseSkill();
            coolDownTimer=coolDown;
            return true;

        }
        
        else
        {
            player.fx.CreatePopUpText("In Cooldown");
            //Debug.Log("Skill is on COOLDOWN");
            return false;
        }
    }

    protected virtual void UseSkill()
    {

    }

    protected virtual void  CheckUnlock()
    {

    }
}
