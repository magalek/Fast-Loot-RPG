using System;
using RPG.Entities;

namespace RPG.Events {
    public class BattleEvents {

        public delegate void BattleDelegate(Player player, Enemy enemy);
        public delegate void ActionDelegate(Character performer, AttackInfo attackInfo);

        public static event BattleDelegate BattleStart;
        public static event ActionDelegate ActionDone;
        public static event Action TurnEnd;

        public static void OnBattleStart(Player player, Enemy enemy) => BattleStart?.Invoke(player, enemy);
        public static void OnActionDone(Character performer, AttackInfo attackInfo) => ActionDone?.Invoke(performer, attackInfo);
        public static void OnTurnEnd() => TurnEnd?.Invoke();
    }
}
