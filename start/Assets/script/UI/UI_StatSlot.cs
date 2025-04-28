using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    private UI ui;
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;


    [TextArea]
    [SerializeField] private string statDescription;
    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;
        if(statNameText!=null)
        {
            statNameText.text = statName;
        }
    }
    

    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if(playerStats!=null)
        {
            statValueText.text = playerStats.StatOfType(statType).GetValue().ToString();


          //Stat change with equipment
            if (statType== StatType.maxHp)
              statValueText.text = playerStats.GetMaxHP().ToString();

            if (statType== StatType.damage)
              statValueText.text = (playerStats.damage.GetValue() + playerStats.strength.GetValue()).ToString();

            if (statType ==StatType.critPower)
                statValueText.text = (playerStats.critPower.GetValue() + playerStats.strength.GetValue()).ToString();

            if (statType ==StatType.critChance)
                statValueText.text = (playerStats.critChance.GetValue() + playerStats.agility.GetValue()).ToString();

            if (statType== StatType.evasion)
                statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();

            if (statType ==StatType.magicResistance)
                statValueText.text = (playerStats.magicResistance.GetValue() + (playerStats.intelligence.GetValue() * 3)).ToString();

        }
    }

    private void Start()
    {
        UpdateStatValueUI();

        ui = GetComponentInParent<UI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowStatToolTip(statDescription);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.HideStatToolTip();
    }
}
