using RPG.Entities;
using RPG.UI;
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
        
        private Entity owner;

        private void Awake() {
            isEmpty = true;
            item = null;
            itemSpriteSlot.GetComponent<Image>().color = Color.clear;
        }
        
        public void HandleAddedItem(Item addedItem)
        {
            if (addedItem == null)
                return;
            
            isEmpty = false;
            item = addedItem;
            itemSpriteSlot.GetComponent<Image>().sprite = item?.sprite;
            itemSpriteSlot.GetComponent<Image>().color = Color.white;
            slotBorder.color = item.color;
        }

        public void RemoveItem(bool destroy = false)
        {
            item = null;
            isEmpty = true;
            itemSpriteSlot.GetComponent<Image>().sprite = null;
            itemSpriteSlot.GetComponent<Image>().color = Color.clear;
            slotBorder.color = Color.black;        
        }

        protected virtual void SlotLeftButtonClick() { }
        protected virtual void SlotRightButtonClick() { }
        protected virtual void SlotMiddleButtonClick() { }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    SlotLeftButtonClick();
                    break;
                case PointerEventData.InputButton.Middle:
                    SlotMiddleButtonClick();
                    break;
                case PointerEventData.InputButton.Right:
                    SlotRightButtonClick();
                    break;
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (item != null)
                ItemTooltip.ChangeTooltip(item.stats.ToString());
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            ItemTooltip.ChangeTooltip("");
        }
    }
}
