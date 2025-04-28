using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimTrigger : MonoBehaviour
{

    private Player player =>GetComponentInParent<Player>();
    
    private void AnimationTrigger()
    {
        player.AnimationTrigger(); 
    }

    private void AttackTrigger()//´¥·¢¹¥»÷
    {
        Collider2D[] colliders=Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats target = hit.GetComponent<EnemyStats>();
                hit.GetComponent<Enemy>().DamageEffect();
                player.stats.DoDamage(target);

                //hit.GetComponent<CharacterStats>().TakeDamage(player.stats.damage.GetValue());
                //Debug.Log("The Damage is + " + player.stats.damage.GetValue());//the value of damage

                //get weapon item 

                ItemData_Equipment weaponData = Inventory.instance.GetEquipmentByType(EquipmentType.Weapon);
                //Debug.Log("TEST: " + target.transform);
                if (weaponData != null)
                {
                    weaponData.ExecuteItemEffect(target.transform);
                     
                }
            }
        }
             
    }
    private void ThrowSword()
    {
        SkillManager.instance.swordSkill.CreateSword();
    }


    //counter test
    private void OpenPCounter()=>player.OpenPCounterWindow();

    private void ClosePCounter()=>player.CloseCounterWindow();


}
