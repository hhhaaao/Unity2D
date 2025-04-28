using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class InventoryItem//同一物品可堆叠多个
{
    public ItemData data;
    public int stackSize;
    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
        AddStack();
    }

    public void AddStack()=>stackSize++;

    public void RemoveStack()=>stackSize--;
    

}
