using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventHandler {

    public delegate void BattleDelegate(Player player, Enemy enemy);
    public delegate void ActionDelegate(Entity performer, AttackInfo attackInfo);

    public static event BattleDelegate BattleStart;
    public static event ActionDelegate ActionDone;

    public static void OnBattleStart(Player player, Enemy enemy)
    {
        BattleStart?.Invoke(player, enemy);
    }

    public static void OnActionDone(Entity performer, AttackInfo attackInfo)
    {
        ActionDone?.Invoke(performer, attackInfo);
    }

}
