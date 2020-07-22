using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Controllers;
using RPG.Entities;
using RPG.Items;
using RPG.Items.Slots;
using UnityEngine;

namespace RPG {
    public class NPCContainer : Container {
        
        private List<Slot> slots;

        private void Awake() {
            slots = GetComponentsInChildren<Slot>().ToList();
        }

        public override void Add(Item item) {
            Slot slot = slots.FirstOrDefault(s => s.isEmpty);
            
            if (slot == null) return;
            
            slot.Insert(item);
            
            Score.Instance.AddScore(item.value);
        }

        // public override void Remove(Item item) {
        //     // Slot slot = slots.FirstOrDefault(s => s.item == item);
        //     //
        //     // if (slot == null) return;
        //     //
        //     // slot.RemoveItem();
        //     //
        //     // if (Target != null) {
        //     //     Target.Add(item);
        //     // }
        //     // else {
        //     //     ItemsController.Instance.CreateItemObject(item, Player.Instance.transform.position);
        //     // }
        // }
    }
}