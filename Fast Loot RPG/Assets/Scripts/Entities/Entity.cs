using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public string entityName;

    public Statistics statistics;

    public AbilityManager abilityManager = new AbilityManager();
    public EffectManager effectManager = new EffectManager();

    public virtual void Kill()
    {
        abilityManager.DetachEvents();
        Destroy(gameObject);
    }

}
