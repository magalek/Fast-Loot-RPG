using RPG.Entities;
using RPG.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Items.Slots
{
    public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

        public bool isEmpty = true;
        public Item item;
        public SlotGraphics slotGraphics;

        private bool showTooltip;

        private string tooltip;
        
        private Container container;
        
        protected virtual void Awake() {
            slotGraphics = new SlotGraphics(this);
            item = null;
            container = GetComponentInParent<Container>();
        }

        private void Start() {
            //Player.Instance.GetComponentInChildren<PlayerUI>().CharacterInfoHidden += () => showTooltip = false;
        }

        public void Insert(Item addedItem) {
            if (addedItem == null)
                return;
            
            item = addedItem;
            isEmpty = false;
            
            slotGraphics.Change(addedItem);
        }

        public void RemoveItem() {
            item = null;
            isEmpty = true;
           
            slotGraphics.Change();
        }

        protected virtual void OnLeftMouseClick() { }

        protected virtual void OnRightMouseClick() {
            if (item != null) container.Remove(item);
        }
        protected virtual void OnMiddleMouseClick() { }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnLeftMouseClick();
                    break;
                case PointerEventData.InputButton.Middle:
                    OnMiddleMouseClick();
                    break;
                case PointerEventData.InputButton.Right:
                    OnRightMouseClick();
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            tooltip = item?.ToString();
            showTooltip = true;
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            tooltip = null;
            showTooltip = false;
        }
        
        private void OnGUI() {
            if (item == null) return;
            
            if (showTooltip) {
                CreateTooltip();
            }
        }

        private void CreateTooltip() {
            if (tooltip == null || showTooltip == false) return;
            
            GUIContent content = new GUIContent(tooltip);
            GUIStyle style = GUI.skin.box;

            style.fontSize = 18;
            
            Vector2 pos = new Vector2(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y);
            Vector2 size = style.CalcSize(content);

            GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), content, style);
        }
    }
}
