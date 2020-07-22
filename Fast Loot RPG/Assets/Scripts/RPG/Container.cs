using System;
using System.Collections.Generic;
using RPG.Items;
using RPG.Items.Slots;
using UnityEngine;

namespace RPG {
    public abstract class Container : MonoBehaviour {

        public event Action<bool, Container> OnStateChange;
        
        public Container Target { get; set; }

        public void ChangeState(bool state) => ContainerController.Instance.ChangeContainerState(this, state);

        public virtual void Add(Item item) { }
        public virtual void Remove(Item item) { }
    }
}