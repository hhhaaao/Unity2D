using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    protected override void Start()
    {
        base.Start();
    }

    public void SetUpCraftSlot(ItemData_Equipment _data)
    {
        if(_data == null)
            return;
        item.data = _data;
        itemText.text = _data.itemName;
        itemImage.sprite = _data.icon;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
            //ItemData_Equipment craftData = item.data as ItemData_Equipment;
            //Inventory.instance.CanCraft(craftData, craftData.craftMaterials);

        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);


    }
}
