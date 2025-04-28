using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's Drop")]
    [SerializeField] private float chanceToLoseItems;
    [SerializeField] private float chanceToLoseMaterials;

    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.instance;
        //list of equipment


        List<InventoryItem> currentEquipment = inventory.GetEquipmentList();
        List<InventoryItem> itemsToUnEquip = new List<InventoryItem>();

        List<InventoryItem> currentStash=inventory.GetStashList();
        List<InventoryItem> materialsToLose= new List<InventoryItem>();


        foreach(InventoryItem item in currentEquipment)//foreach里不能直接修改数组
        {
            if(Random.Range(0,100)<chanceToLoseItems)
            {
                DropItem(item.data);
                itemsToUnEquip.Add(item);
            }
        }

        for(int i=0;i<itemsToUnEquip.Count;i++)
        {
            inventory.UnEquipItem(itemsToUnEquip[i].data as ItemData_Equipment);
           

        }

        foreach (InventoryItem item in currentStash)//foreach里不能直接修改数组
        {
            if (Random.Range(0, 100) < chanceToLoseMaterials)
            {
                DropItem(item.data);
                materialsToLose.Add(item);
            }
        }

        for (int i = 0; i < materialsToLose.Count; i++)
        {
            inventory.RemoveItem(materialsToLose[i].data);

        }

    }

}
