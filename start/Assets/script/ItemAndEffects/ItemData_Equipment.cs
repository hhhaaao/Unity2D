using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
    #region Basic Stats
    [Header("Major Stats")]
    public int strength;//increase damage by 1 and crit.damage by 1
    public int agility;//increase evasion by 1% and crit.chance by 1%
    public int intelligence;//increase magic damage by 1 and magic.resistance by 1%
    public int vitality;//increase health by 1

    
    [Header("Defensive Stats")]
    public int maxHp;
    public int armor;
    public int evasion;
    public int magicResistance;
  

    
    [Header("Offensive Stats")]
    public int damage;
    public int critChance;
    public int critPower;
    

   
    [Header("Magic Stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightningDamage;
    #endregion

    

    [Header("Craft Requirements")]
    public List<InventoryItem> craftMaterials;

    [Header("Unique effect")]
    //public float itemCooldown;
    public ItemEffect[] itemEffect;
    //[TextArea]
    //public string ItemEffectDescription;

    private int descriptionLength;
    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.armor.AddModifier(armor);
        playerStats.maxHp.AddModifier(maxHp);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightningDamage.AddModifier(lightningDamage);


    }

    public void RemoveModifers()
    {

        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.armor.RemoveModifier(armor);
        playerStats.maxHp.RemoveModifier(maxHp);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightningDamage.RemoveModifier(lightningDamage);
    }

    public void ExecuteItemEffect(Transform _enemyPosition)
    {
        foreach (var item in itemEffect)
        {
            item.ExecuteEffect(_enemyPosition);
        }
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;
        AddItemDescription(strength, "strength");
        AddItemDescription(agility, "agility");
        AddItemDescription(intelligence, "intelligence");
        AddItemDescription(vitality, "vitality");
        AddItemDescription(armor, "armor");
        AddItemDescription(maxHp, "maxHp");
        AddItemDescription(magicResistance, "magicResistance");
        AddItemDescription(evasion, "evasion");
        AddItemDescription(damage, "damage");
        AddItemDescription(critChance, "critChance");
        AddItemDescription(critPower, "critPower"); 
        AddItemDescription(fireDamage, "fireDamage");
        AddItemDescription(iceDamage, "iceDamage");
        AddItemDescription(lightningDamage, "lightningDamage");



        for (int i = 0; i < itemEffect.Length; i++)
        {
            if (itemEffect[i].effectDescription.Length > 0)
            {
                sb.AppendLine();
                sb.Append(itemEffect[i].effectDescription);
                descriptionLength++;
            }
        }


        if(descriptionLength<5)
        {
            for(int i=0;i<5-descriptionLength;i++)
            {
                sb.AppendLine();
                sb.Append(" ");
            }
        }


      
        return sb.ToString();
    }

    private void AddItemDescription(int _value,string _name)
    {
        if(_value!=0)
        {
            if (sb.Length > 0)
                sb.AppendLine();
            if (_value > 0)
                sb.Append("+ "+_value+" "+_name);

            descriptionLength++;


            
        }
    }
}

