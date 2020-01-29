using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public string entityName;

    public Statistics statistics;

    public AbilityManager abilityManager;
    public EffectManager effectManager;

    private void Awake()
    {
        effectManager = new EffectManager(this);
    }

    public virtual void Kill()
    {
        abilityManager.DetachEvents();
        Destroy(gameObject);
    }

}
