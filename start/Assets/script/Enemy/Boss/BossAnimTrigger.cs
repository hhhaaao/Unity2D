using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimTrigger : Enemy_AnimTrigger
{
    private EnemyBoss boss => GetComponentInParent<EnemyBoss>();

    public LayerMask player;

    private void Relocate() => boss.FindPosition();

    private void MakeInvisiable() => boss.fx.MakeTransparent(true);

    private void MakeVisiable() => boss.fx.MakeTransparent(false);

    //private void AttackTrigger()
    //{
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(boss.attackCheck.position, boss.attackCheckRadius,player);
    //    Debug.Log($"AttackTrigger detected {colliders.Length} objects.");
    //    foreach (var a in colliders)
    //    {
    //        Debug.Log($"Hit object: {a.name}");
    //    }

    //    foreach (var a in colliders)
    //        if (a.GetComponent<Player>() != null)
    //        {
    //            CharacterStats target = a.GetComponent<PlayerStats>();
    //            a.GetComponent<Player>().DamageEffect();
    //            boss.stats.DoDamage(target);
    //        }
    //}

    private void AttackTrigger()
    {
        int playerLayer = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(boss.attackCheck.position, boss.attackCheckRadius, playerLayer);

        Debug.Log($"AttackTrigger detected {colliders.Length} objects.");

        foreach (var a in colliders)
        {
            Debug.Log($"Hit object: {a.name}, Layer: {a.gameObject.layer}");
        }

        //foreach (var a in colliders)
        //{
        //    Player player = a.GetComponent<Player>(); // 先存变量，避免重复调用
        //    if (player != null)
        //    {
        //        CharacterStats target = a.GetComponent<PlayerStats>();
        //        player.DamageEffect();
        //        boss.stats.DoDamage(target);
        //    }
        //}
        foreach (var a in colliders)
        {
            Player player = a.GetComponent<Player>();
            if (player != null)
            {
                CharacterStats target = a.GetComponent<PlayerStats>();
                Debug.Log("Calling DoDamage()");
                boss.stats.DoDamage(target);
            }
        }

    }


}
