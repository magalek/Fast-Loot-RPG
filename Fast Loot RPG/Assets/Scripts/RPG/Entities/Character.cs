using System;
using RPG.Statistics;
using UnityEngine;
using CharacterInfo = RPG.Statistics.CharacterInfo;

namespace RPG.Entities
{
    [RequireComponent(typeof(CharacterInfo))]
    public class Character : MonoBehaviour {

        public string entityName;

        public CharacterInfo characterInfo;

        public void Awake() {
            characterInfo = GetComponent<CharacterInfo>();
        }

        public virtual void Kill() {
            GameObject.Find("Amount Text").GetComponent<Score>().AddScore(20);
            Destroy(gameObject);            
        }
    }
}
