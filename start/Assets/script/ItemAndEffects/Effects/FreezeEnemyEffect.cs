using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Freeze Enemies Effect", menuName = "Data/ItemEffect/FreezeEnemies")]
public class FreezeEnemyEffect : ItemEffect
{
    [SerializeField] private float duration;
    public override void ExecuteEffect(Transform _enemyPositon)
    {
        PlayerStats playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();
       if(playerStats.currentHealth>playerStats.GetMaxHP()*0.1)
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_enemyPositon.position,2);
        
        foreach (var hit in colliders)
        {
            
            hit.GetComponent<Enemy>()?.FreezeTimeFor(duration);
            
        }
    }
}
