using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder Strike Effect", menuName = "Data/ItemEffect/ThunderStrike")]
public class ThunderStrikeEffect : ItemEffect
{

    
    [SerializeField] private GameObject thunderStrikePerfab;
    public override void ExecuteEffect(Transform _enemyPositon)
    {
        GameObject newThunderStrike = Instantiate(thunderStrikePerfab,_enemyPositon.position,Quaternion.identity);

        Destroy(newThunderStrike,1f);
    }
}
