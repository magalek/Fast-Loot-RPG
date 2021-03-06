﻿using RPG.Entities;

namespace RPG.Events {
    public class EnemyEvents : EntityEvents
    {

        public delegate void EnemyDelegate(Enemy enemy);

        public static event EnemyDelegate EnemyDeath;
        public static event EnemyDelegate EnemyHit;

        public static void OnEnemyHit(Enemy enemy)
        {
            EnemyHit?.Invoke(enemy);
        }

        public static void OnEnemyKilled(Enemy enemy)
        {
            EnemyDeath?.Invoke(enemy);
        }
    }
}
