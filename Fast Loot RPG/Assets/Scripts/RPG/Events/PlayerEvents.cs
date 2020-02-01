using System;
using RPG.Entities;

namespace RPG.Events {
    public class PlayerEvents : EntityEvents {

        public static event Action<Player> PlayerHit;
        public static event Action PlayerDeath;

        public static void OnPlayerHit(Player player) => PlayerHit?.Invoke(player);
        public static void OnPlayerDeath(Player player) => PlayerDeath?.Invoke();
    }
}
