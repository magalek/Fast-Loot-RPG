using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Controllers;
using RPG.Entities;
using RPG.Events;
using RPG.Items.Slots;
using UnityEngine;

namespace RPG.Items
{
    public class Inventory : MonoBehaviour {

        private List<InventorySlot> slots;

        private void Awake() {
            slots = GetComponentsInChildren<InventorySlot>().ToList();
        }

        public void Add(Item item) {
            InventorySlot slot = slots.FirstOrDefault(s => s.isEmpty);

            if (slot == null) return;

            slot.Insert(item);
        }

        public void Remove(Item item) {
            InventorySlot slot = slots.FirstOrDefault(s => s.item == item);

            if (slot == null) return;

            slot.RemoveItem();
            ItemsController.Instance.CreateItemObject(item, Player.Instance.transform.position);
        }
    }
}


