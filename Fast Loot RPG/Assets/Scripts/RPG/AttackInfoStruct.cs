using RPG.Entities;

/// <summary>
/// Info about damage, attack type and target of the attack
/// </summary>
public struct AttackInfo
{
    public int damage;
    public AttackType type;
    public Entity target;

    public AttackInfo(int _damage, AttackType _type, Entity _target)
    {
        damage = _damage;
        type = _type;
        target = _target;
    }
}

public enum AttackType
{
    None,
    Normal,
    Critical,
    Spell
}
