using System;
using UnityEngine;

namespace RPG {
    public interface IInteractable {
        void Interact(GameObject client);
    }
}