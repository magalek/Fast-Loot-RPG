using System.Collections.Generic;
using System.Linq;
using RPG.Controllers;
using RPG.Entities;
using RPG.Items.Slots;
using UnityEngine;

namespace RPG.Items
{
    public class Inventory : Container {

        private List<InventorySlot> slots;

        private void Awake() {
            slots = GetComponentsInChildren<InventorySlot>().ToList();
        }

        public override void Add(Item item) {
            InventorySlot slot = slots.FirstOrDefault(s => s.isEmpty);

            if (slot == null) return;

            slot.Insert(item);
        }

        public override void Remove(Item item) {
            InventorySlot slot = slots.FirstOrDefault(s => s.item == item);

            if (slot == null) return;

            slot.RemoveItem();
            if (Target != null) {
                Target.Add(item);
            }
            else {
                ItemsController.Instance.CreateItemObject(item, Player.Instance.transform.position);
            }
        }
    }
}


