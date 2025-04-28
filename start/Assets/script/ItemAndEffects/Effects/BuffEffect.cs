using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/ItemEffect/Buff")]
public class BuffEffect : ItemEffect
{
    PlayerStats stats;
    [SerializeField] private int buffAmount;
    [SerializeField] private int buffDuration;
    [SerializeField] private StatType buffType;
    public override void ExecuteEffect(Transform _enemyPositon)
    {
        stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        stats.IncreaseStatsBy(buffAmount, buffDuration, stats.StatOfType(buffType));

    }

    //private Stat StatToModify()
    //{
    //    switch(buffType)
    //    {
    //        case StatType.strength: return stats.strength;
    //        case StatType.agility: return stats.agility; 
    //        case StatType.intelligence: return stats.intelligence; 
    //        case StatType.vitality: return stats.vitality; 
    //        case StatType.damage: return stats.damage; 
    //        case StatType.critChance: return stats.critChance; 
    //        case StatType.critPower: return stats.critPower; 
    //        case StatType.evasion: return stats.evasion; 
    //        case StatType.maxHp: return stats.maxHp; 
    //        case StatType.armor: return stats.armor; 
    //        case StatType.magicResistance: return stats.magicResistance; 
    //        case StatType.fireDamage: return stats.fireDamage; 
    //        case StatType.iceDamage: return stats.iceDamage; 
    //        case StatType.lightningDamage: return stats.lightningDamage; 
    //        default:
    //            return null;
    //    }
            
    //}
}
