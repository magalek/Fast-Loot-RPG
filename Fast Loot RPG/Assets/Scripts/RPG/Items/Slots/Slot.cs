using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Items.Slots
{
    public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject itemSpriteSlot;
        [SerializeField] Image slotBorder;

        public bool isEmpty;
        private Button slotButton;
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

        public void RemoveItem(bool destroy = false)
        {
            item = null;
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

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (item)
                ItemTooltip.ChangeTooltip(item.statistics.ToString());
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            ItemTooltip.ChangeTooltip("");
        }
    }
}
