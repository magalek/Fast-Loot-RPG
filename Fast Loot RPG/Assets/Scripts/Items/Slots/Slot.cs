using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    [SerializeField] GameObject itemSpriteSlot;
    [SerializeField] Image slotBorder;

    public bool isEmpty;

    public Item item;

    public void HandleAddedItem(Item addedItem)
    {
        isEmpty = false;
        item = addedItem;
        if (item != null)
        {
            itemSpriteSlot.GetComponent<SpriteRenderer>().sprite = item?.sprite;
            slotBorder.color = item.color;
        }
    }

    public void HandleRemovedItem()
    {
        isEmpty = true;
        itemSpriteSlot.GetComponent<SpriteRenderer>().sprite = null;
        slotBorder.color = Color.black;
    }

    public virtual void SlotClick() { }

}
