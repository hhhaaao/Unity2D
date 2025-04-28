using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;
    public Stat soulsDrop;

    [Header("Enemy Details")]
    [SerializeField]private int enemyLevel=1;

    [Range(0f,1f)]
    [SerializeField]private float percentagModifier=0.315f;
    protected override void Start()
    {
        soulsDrop.SetDefaultValue(100);
        ApplyLevelModifiers();
        base.Start();
       
        
        
        //Debug.Log(gameObject +"totalDamage is "+)
        enemy = GetComponent<Enemy>();

        myDropSystem=GetComponent<ItemDrop>();

    }

    private void ApplyLevelModifiers()
    {
        Modify(strength);
        Modify(fireDamage);

        Modify(soulsDrop);
    }

    private void Modify(Stat _stat)//enemy enhanced with level
    {
        for(int i=1;i<enemyLevel;i++)
        {
            float modifier = _stat.GetValue() * percentagModifier;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        enemy.DamageEffect();
    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();
        PlayerManager.instance.currency += soulsDrop.GetValue();
        myDropSystem.GenerateDrop();

        Destroy(gameObject,5f);

    }
}
