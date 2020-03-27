using RPG.Entities;
using RPG.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Items.Slots
{
    public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

        public bool isEmpty = true;
        public Item item;
        
        private Entity owner;
        public SlotGraphics slotGraphics;

        private void Awake() {
            slotGraphics = new SlotGraphics(this);
        }
        
        public void InsertItem(Item addedItem) {
            if (addedItem == null)
                return;
            
            item = addedItem;
            isEmpty = false;
            
            slotGraphics.Change(addedItem);
        }

        public void RemoveItem(bool destroy = false) {
            item = null;
            isEmpty = true;
           
            slotGraphics.Change();
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
