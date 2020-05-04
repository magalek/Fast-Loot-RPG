using System;
using RPG.Statistics;
using UnityEngine;

namespace RPG.Entities
{
    [RequireComponent(typeof(Info))]
    public class Character : MonoBehaviour {

        public string entityName;

        public Info info;

        public void Awake() {
            info = GetComponent<Info>();
        }

        public virtual void Kill() {
            Destroy(gameObject);            
        }
    }
}
