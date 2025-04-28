using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 velocity;


    



    private void SetupVisual()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object-" + itemData.itemName;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
            rb.velocity=velocity;
    }

    public void SetUpItem(ItemData _itemdata,Vector2 _velocity)
    {
        itemData = _itemdata;
        rb.velocity=_velocity;
        SetupVisual();
    }

    public  void PickUpItem()
    {
        if (!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0, 7);
            PlayerManager.instance.player.fx.CreatePopUpText("No Enough Slots");
        return;
        } 
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
