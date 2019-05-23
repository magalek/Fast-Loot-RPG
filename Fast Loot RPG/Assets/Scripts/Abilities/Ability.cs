using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {    

    public virtual AttackInfo Invoke(Entity performer = null, Entity target = null) { return new AttackInfo(0, AttackType.None, null); }

}


