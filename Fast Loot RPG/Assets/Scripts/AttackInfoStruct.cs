using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo
{
    public int damage;
    public AttackType type;
    public Entity target;

    public AttackInfo(int damage, AttackType type, Entity target)
    {
        this.damage = damage;
        this.type = type;
        this.target = target;
    }
}

public enum AttackType
{
    None,
    Normal,
    Critical,
    Spell
}
