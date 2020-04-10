﻿using System;
using RPG.Entities.Animations;
using RPG.Entities.Movement;
using RPG.Items;
using RPG.Utility;
using UnityEngine;

namespace RPG.Entities
{
    public class Player : Character, IComponentCache, IHittable {
        public static event Action Spawned;
        public static event Action Died;

        public static Player Instance;

        public Equipment equipment;
        
        public PlayerController playerController;
        public PlayerAnimationController animationController;
        
        private void Awake() {
            base.Awake();
            
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            
            CacheComponents();

            DontDestroyOnLoad(gameObject);
            Spawned?.Invoke();
        }

        public virtual void Hit(int damage) {
            health.Subtract(damage);
            animationController.PlayHit();
            
            if (health.zeroOrLess)
                Kill();
        }
        
        public override void Kill() {
            base.Kill();
            Died?.Invoke();
        }

        public void CacheComponents() {
            playerController = GetComponent<PlayerController>();
            animationController = GetComponent<PlayerAnimationController>();
        }
    }
}
