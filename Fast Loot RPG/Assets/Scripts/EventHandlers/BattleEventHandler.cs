using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventHandler {

    public delegate void BattleDelegate(Player player, Enemy enemy);

    public static event BattleDelegate BattleStart;

    public static void OnBattleStart(Player player, Enemy enemy)
    {
        BattleStart?.Invoke(player, enemy);
    }

}
