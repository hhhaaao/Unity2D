using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_ItemToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private int defaultFontSize = 32;
    void Start()
    {
        
    }

    public void ShowToolTip(ItemData_Equipment _item)
    {
        itemNameText.text = _item.itemName;
        itemTypeText.text=_item.equipmentType.ToString();
        itemDescription.text = _item.GetDescription();

        AdjustFontSize(itemNameText);
        AdjustPosition();




        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        itemNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }
}
