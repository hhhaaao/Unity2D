using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory instance;
    public List<ItemData> startingEquipment;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;

    private UI_ItemSlot[] inventoryItemSlots;
    private UI_ItemSlot[] stashItemSlots;
    private UI_EquipmentSlot[] equipmentItemSlots;
    private UI_StatSlot[] statSlots;

    [Header("Items Cooldown")]

    private float lastTimeUsedflask = float.MinValue;
    private float lastTimeUsedArmor = float.MinValue;

    public float flaskCooldown;
    public float armorCooldown;


    [Header("Data Base")]
    
    public List<ItemData> itemDataBase;
    public List<InventoryItem> loadedItems;
    public List<ItemData_Equipment> loadedEquipment;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        inventoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();
        stashItemSlots = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();
        equipmentItemSlots = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

        statSlots = statSlotParent.GetComponentsInChildren<UI_StatSlot>();
        AddStartingItems();
    }

    private void AddStartingItems()
    {
        foreach (ItemData_Equipment item in loadedEquipment)
        {
        
            EquipItem(item);
            //Debug.Log(item.itemID);
        }

        if (loadedItems.Count > 0)
        {
            foreach (InventoryItem item in loadedItems)
            {
                for (int i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.data);
                }
            }

            return;
        }
        Debug.Log("no starting");
        for (int i = 0; i < startingEquipment.Count; i++)
        {
            AddItem(startingEquipment[i]);

        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment itemToUnEquip = null;

        //如果已有同类型装备，删除
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)//已装备列表中有同类型装备
            {
                itemToUnEquip = item.Key;
                //equipmentDictionary.Remove(itemToUnEquip);//有这个无法删除
            }
        }
        if (itemToUnEquip != null)
        {

            UnEquipItem(itemToUnEquip);
            AddItem(itemToUnEquip);
        }
        //装备栏增加
        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();
        //从库存中删去
        RemoveItem(_item);

        UpdateSlotUI();
    }

    public void UnEquipItem(ItemData_Equipment itemToUnEquip)
    {
        if (equipmentDictionary.TryGetValue(itemToUnEquip, out InventoryItem value))
        {

            equipment.Remove(value);
            equipmentDictionary.Remove(itemToUnEquip);
            Debug.Log(itemToUnEquip.itemName + "is removed");
            itemToUnEquip.RemoveModifers();
        }
        UpdateSlotUI();


    }

    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment && CanAddItem())
        {
            AddToInventory(_item);

        }
        else if (_item.itemType == ItemType.Material)
        {
            AddToStash(_item);
        }

        UpdateSlotUI();
    }//item->inventroyItems

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    public void UpdateSlotUI()
    {
        //每次更新前清空所有slot
        for (int i = 0; i < inventoryItemSlots.Length; i++)
        {
            inventoryItemSlots[i].CleanUpSlot();
        }

        for (int i = 0; i < equipmentItemSlots.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentItemSlots[i].slotType)
                {
                    equipmentItemSlots[i].UpdateSlot(item.Value);

                }
            }
        }


        for (int i = 0; i < stashItemSlots.Length; i++)
        {
            stashItemSlots[i].CleanUpSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlots[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlots[i].UpdateSlot(stash[i]);
        }

        for (int i = 0; i < statSlots.Length; i++)
        {
            statSlots[i].UpdateStatValueUI();
        }



    }//inventoryItems->itemSlots

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else if (value.stackSize > 1)
            {
                value.RemoveStack();
            }
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else if (stashValue.stackSize > 1)
            {
                stashValue.RemoveStack();
            }
        }



        UpdateSlotUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inventory.Count == 0)
            {
                Debug.Log("Inventory is empty");
                return;
            }
            ItemData _data = inventory[inventory.Count - 1].data;
            RemoveItem(_data);

        }
    }

    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials)
    {
        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            Debug.Log("requirelist: " + _requiredMaterials[i].stackSize);
        }


        List<InventoryItem> materialsToRemove = new List<InventoryItem>();

        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            if (stashDictionary.TryGetValue(_requiredMaterials[i].data, out InventoryItem stashValue))
            {
                //addto
                if (stashValue.stackSize < _requiredMaterials[i].stackSize)
                {
                    Debug.Log("NOT ENOUGH Material");
                    return false;
                }
                else
                {
                    //Debug.Log(stashValue.data.name + " require " + stashValue.stackSize);
                    materialsToRemove.Add(_requiredMaterials[i]);
                }

            }
            else
            {
                Debug.Log("NOT ENOUGH Material");
                return false;
            }

        }

        //consume material
        for (int i = 0; i < materialsToRemove.Count; i++)
        {
            for (int j = 0; j < materialsToRemove[i].stackSize; j++)
            {
                RemoveItem(materialsToRemove[i].data);
            }
        }

        AddItem(_itemToCraft);
        Debug.Log("Craft " + _itemToCraft.name + " successfully");
        return true;
    }

    public List<InventoryItem> GetEquipmentList() => equipment;

    public List<InventoryItem> GetStashList() => stash;

    public ItemData_Equipment GetEquipmentByType(EquipmentType _type)
    {
        ItemData_Equipment equippedItem = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
            {
                equippedItem = item.Key;
                //equipmentDictionary.Remove(itemToUnEquip);//有这个无法删除
            }


        }
        return equippedItem;
    }

    public void UseFlask()
    {
        ItemData_Equipment currentFlask = GetEquipmentByType(EquipmentType.Flask);
        if (currentFlask == null)
        {
            Debug.Log("no flask");
            return;
        }

        bool canUseFlask = Time.time > lastTimeUsedflask + flaskCooldown;

        if (canUseFlask)
        {
            currentFlask.ExecuteItemEffect(null);
            lastTimeUsedflask = Time.time;
        }
        else
        {
            Debug.Log("Flask is in cd");
        }
    }

    public bool CanAddItem()
    {
        if (inventory.Count >= inventoryItemSlots.Length)
        {
            Debug.Log("no available slots");
            return false;
        }
        else
            return true;
    }
    public bool CanUseArmor()
    {
        ItemData_Equipment currentArmor = GetEquipmentByType(EquipmentType.Armor);
        if (currentArmor != null)
        {
            if (Time.time > lastTimeUsedArmor + armorCooldown)
            {
                lastTimeUsedArmor = Time.time;
                return true;
            }
            else
            {
                Debug.Log("Armor On Cooldown");
                return false;

            }

        }
        else
        {
            return false;
        }


    }

    //save
    public void LoadData(GameData _data)
    {
        //throw new System.NotImplementedException();
        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemID == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadedItems.Add(itemToLoad);
                }
            }
        }

        foreach (string itemId in _data.equipmentID)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && itemId == item.itemID)
                {
                    //Debug.Log("load equip " + item.name);
                    loadedEquipment.Add(item as ItemData_Equipment);

                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipmentID.Clear();
        foreach (KeyValuePair<ItemData, InventoryItem> pair in inventoryDictionary)
        {
            _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemData, InventoryItem> pair in stashDictionary)
        {
            _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> pair in equipmentDictionary)
        {
            _data.equipmentID.Add(pair.Key.itemID);
        }
    }



#if UNITY_EDITOR
    [ContextMenu("Fill up item data Base")]
    private void FillupDataBase()=>itemDataBase=new List<ItemData>(GetItemDataBase()); 
    private List<ItemData> GetItemDataBase()
    {
        itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" }); 

        foreach (string SOname in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOname);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }
        return itemDataBase;
    } 
#endif
}
