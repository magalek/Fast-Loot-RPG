using UnityEngine;
using CharacterInfo = RPG.Statistics.CharacterInfo;

namespace RPG.Entities
{
    [RequireComponent(typeof(CharacterInfo))]
    public class Character : MonoBehaviour {

        public CharacterInfo characterInfo;

        public virtual void Awake() {
            characterInfo = GetComponent<CharacterInfo>();
        }

        protected virtual void Kill() {
            Score.Instance.Amount += (int)((characterInfo.Health.Max + characterInfo.Damage.Current) * 0.2f);
            Destroy(gameObject);            
        }
    }
}
