using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        //Debug.Log("TOTALDAMAGE IS "+damage.GetValue());
        player = GetComponent<Player>();

        
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        player.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
        Debug.Log("die: " + PlayerManager.instance.currency);
        GameManager.instance.LostCurrencyAmount = PlayerManager.instance.currency;

        PlayerManager.instance.currency = 0;

        GetComponent<PlayerItemDrop>().GenerateDrop();
    }

    protected override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        if (_damage > GetMaxHP() * 0.3)
        {
            player.SetupKnockbackPower(new Vector2(5, 7));
            player.fx.ScreenShake(player.fx.shakeHighDamage);
            //Debug.Log("High Damage Taken");

        }

        if (!Inventory.instance.CanUseArmor())
            return;
        ItemData_Equipment currentArmor=Inventory.instance.GetEquipmentByType(EquipmentType.Armor);
        if(currentArmor!=null)
        {

            currentArmor.ExecuteItemEffect(player.transform);
        }
    }
}
