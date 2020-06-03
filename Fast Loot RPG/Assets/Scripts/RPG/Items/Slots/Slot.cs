﻿using System;
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

        private bool showTooltip = false;
        
        protected virtual void Awake() {
            slotGraphics = new SlotGraphics(this);
            item = null;

        }

        private void Start() {
            Player.Instance.GetComponentInChildren<PlayerUI>().CharacterInfoHidden += () => showTooltip = false;
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

        public void OnPointerEnter(PointerEventData eventData) {
            showTooltip = true;
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            showTooltip = false;
        }
        
        private void OnGUI() {
            if (item == null) return;
            
            if (showTooltip) {
                CreateTooltip();
            }
        }

        private void CreateTooltip() {
            GUIContent content = new GUIContent(item.ToString());
            GUIStyle style = GUI.skin.box;

            style.fontSize = 18;
            
            Vector2 pos = new Vector2(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y);
            Vector2 size = style.CalcSize(content);

            GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), content, style);
        }
    }
}
