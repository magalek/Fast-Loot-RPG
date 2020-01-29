using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Entities;
using UnityEngine;

public class PlayerEventHandler : EntityEventHandler {


    public static event Action<Player> PlayerHit;
    public static event Action PlayerDeath;

    public static void OnPlayerHit(Player player) => PlayerHit?.Invoke(player);
    public static void OnPlayerDeath(Player player) => PlayerDeath?.Invoke();
}
