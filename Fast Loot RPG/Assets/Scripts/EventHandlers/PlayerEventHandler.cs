using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandler : EntityEventHandler {

    public delegate void PlayerDelegate(Player player);

    public static event PlayerDelegate PlayerHit;

    public static void OnPlayerHit(Player player) => PlayerHit?.Invoke(player);
}
