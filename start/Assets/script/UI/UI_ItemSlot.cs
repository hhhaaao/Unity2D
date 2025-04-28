using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class UI_ItemSlot :MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]protected Image itemImage;
    [SerializeField]protected TextMeshProUGUI itemText;

    public InventoryItem item;

    public UI ui;

    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();

    }

    public void CleanUpSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color= Color.clear;
        itemText.text = "";

    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;//初始化时透明改为1
        if (item != null)
        {
            itemImage.sprite = item.data.icon;

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item==null)
        {
            return;
        }

        if(Input.GetKey(KeyCode.LeftControl))
            {
            Inventory.instance.RemoveItem(item.data);
            return;
        }

        if (item.data.itemType == ItemType.Material)
        {
            Debug.Log("Can't Equip");
            return; }

        //if(item.data.itemType!=ItemType.Material)

        
            Debug.Log("Equipped new item " + item.data.itemName);
            Inventory.instance.EquipItem(item.data);

        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null||item.data==null)
            return;
        //panel moves with mouse
       

      
        ui.itemToolTip.ShowToolTip(item.data as ItemData_Equipment);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null||item.data==null)
            return;
        ui.itemToolTip.HideToolTip();
    }
}
