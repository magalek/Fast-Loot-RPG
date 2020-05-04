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
        public Inventory inventory;
        public Weapon weapon;
        
        public PlayerController playerController;
        public PlayerAnimationController animationController;
        
        public bool IsHittable { get; set; }
        
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
            info.Health.Subtract(damage);
            animationController.PlayHit();
            
            if (info.Health.ZeroOrLess)
                Kill();
        }
        
        public override void Kill() {
            base.Kill();
            Died?.Invoke();
        }

        public void CacheComponents() {
            playerController = GetComponent<PlayerController>();
            animationController = GetComponent<PlayerAnimationController>();
            inventory = GetComponentInChildren<Inventory>();
            equipment = GetComponentInChildren<Equipment>();
            weapon = GetComponentInChildren<Weapon>();
        }
    }
}
