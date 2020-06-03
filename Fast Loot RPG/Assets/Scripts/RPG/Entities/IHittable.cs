using UnityEngine;

namespace RPG.Entities {
    public interface IHittable {
        void Hit(int damage);
        bool IsHittable { get; set; }
    }
}