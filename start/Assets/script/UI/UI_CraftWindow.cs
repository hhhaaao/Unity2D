using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button craftButton;

    [SerializeField] private Image[] materialImage;
    
    public void SetupCraftWindow(ItemData_Equipment _item)
    {
        craftButton.onClick.RemoveAllListeners();
        //wipe out materials when set up
        for(int i=0;i<materialImage.Length;i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color=Color.clear;

        }

        for(int i = 0; i < _item.craftMaterials.Count;i++)
        {
            if(_item.craftMaterials.Count>materialImage.Length)
            {
                Debug.Log("You have more materials than you have craft slots");
            }
                materialImage[i].sprite = _item.craftMaterials[i].data.icon;
                materialImage[i].color = Color.white;
                materialImage[i].GetComponentInChildren<TextMeshProUGUI>().text = _item.craftMaterials[i].stackSize.ToString();
                materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }

        itemIcon.sprite = _item.icon;
        itemName.text  = _item.itemName;
        itemDescription.text = _item.GetDescription();
        craftButton.onClick.AddListener(()=>Inventory.instance.CanCraft(_item, _item.craftMaterials));
    }
}
