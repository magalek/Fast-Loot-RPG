using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventHandler {

    public delegate void BattleDelegate(Player player, Enemy enemy);
    public delegate void DamageDelegate(Entity performer, AttackInfo attackInfo);

    public static event BattleDelegate BattleStart;
    public static event DamageDelegate DamageDone;

    public static void OnBattleStart(Player player, Enemy enemy)
    {
        BattleStart?.Invoke(player, enemy);
    }

    public static void OnDamageDone(Entity performer, AttackInfo attackInfo)
    {
        DamageDone?.Invoke(performer, attackInfo);
    }
}
