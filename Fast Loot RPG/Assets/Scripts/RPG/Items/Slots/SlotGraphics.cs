using UnityEngine;
using UnityEngine.UI;

namespace RPG.Items.Slots {
    public class SlotGraphics {
        private Image spriteImage;
        private Image borderImage;

        public SlotGraphics(Slot slot) {
            spriteImage = slot.transform.Find("Sprite").GetComponent<Image>();
            borderImage = slot.transform.Find("Border").GetComponent<Image>();
            
            spriteImage.color = Color.clear;
        }

        public void Change(Item item) {
            spriteImage.sprite = item.sprite;
            spriteImage.color = Color.white;
            borderImage.color = item.color;
        }

        public void Change() {
            spriteImage.sprite = null;
            spriteImage.color = Color.clear;
            borderImage.color = Color.black;
        }
    }
}