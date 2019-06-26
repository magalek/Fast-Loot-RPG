using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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

    public virtual void SlotLeftButtonClick() { }
    public virtual void SlotRightButtonClick() { }
    public virtual void SlotMiddleButtonClick() { }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            SlotLeftButtonClick();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            SlotMiddleButtonClick();
        else if (eventData.button == PointerEventData.InputButton.Right)
            SlotRightButtonClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(item?.statistics.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited");
    }
}
