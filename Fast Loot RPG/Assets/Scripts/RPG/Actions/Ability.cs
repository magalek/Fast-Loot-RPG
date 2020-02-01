using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.Actions {
    [Serializable]
    public class Ability : Action {
        public virtual AttackInfo Invoke(Entity performer = null, Entity target = null) { return new AttackInfo(0, AttackType.None, null); }

    }
}


