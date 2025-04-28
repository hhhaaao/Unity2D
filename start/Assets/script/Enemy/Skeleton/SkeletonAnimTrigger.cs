using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimTrigger : MonoBehaviour
{
    private EnemySkeleton skeleton => GetComponentInParent<EnemySkeleton>();
    
    private void AnimeTrigger()
    {
        skeleton.AnimeFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skeleton.attackCheck.position, skeleton.attackCheckRadius);
        foreach (var a in colliders)
            if (a.GetComponent<Player>() != null)
            {
                CharacterStats target=a.GetComponent<PlayerStats>();
                a.GetComponent<Player>().DamageEffect();
                skeleton.stats.DoDamage(target);
            }
    }

    private void OpenCounter()=> skeleton.OpenCounterWindow();

    private void CloseCounter()=>skeleton.CloseCounterWindow();



}
