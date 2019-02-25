﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAbility : Ability {

    public string abilityName;

    public Statistics statistics;

    public int cooldown = 0;
    public int maxCooldown;

    public override void Execute(Entity performer, Entity target)
    {
        target.statistics += statistics;

        BattleLog.Instance.SendMessageToBattleLog($"{performer.entityName} used {abilityName}");
        cooldown = maxCooldown;
    }

    
}