using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/ItemEffect/Heal")]
public class HealEffect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent;
    public override void ExecuteEffect(Transform _enemyPositon)
    {
        //player Stats
        PlayerStats playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        //how much to heal
        
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHP() * healPercent);
        
        
        //execute heal
        playerStats.IncreaseHealthBy(healAmount);
       
    }
}
