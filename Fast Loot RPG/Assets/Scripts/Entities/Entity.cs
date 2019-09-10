using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public string entityName;

    public Statistics statistics;

    public AbilityManager abilityManager = new AbilityManager();      

    public virtual void Kill()
    {
        Destroy(gameObject);
    }

}
