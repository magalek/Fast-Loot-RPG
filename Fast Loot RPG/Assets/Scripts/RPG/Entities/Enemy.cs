﻿using RPG.Controllers;
using RPG.Entities.Animations;
using RPG.Entities.Movement;
using RPG.Events;
using RPG.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RPG.Entities
{
    public class Enemy : Character, IHittable, IComponentCache {
        public float lootChance;

        public EnemyAnimationController animationController;
        public EnemyController enemyController;

        public bool IsHittable { get; set; } = true;

        private GameObject bloodPrefab;
        
        private void Awake() {
            base.Awake();
            CacheComponents();
            
            bloodPrefab = Resources.Load<GameObject>("Prefabs/Blood");
        }
        
        public override void Kill() {
            EnemyEvents.OnEnemyKilled(this);
            if (lootChance > Random.value) {
                ItemsController.Instance.CreateItemObject(transform.position, characterInfo);
            }
            GetComponentInChildren<SoundSpawner>().PlayDeathSound();
            base.Kill();
        }

        public void Hit(int damage) {
            if (!IsHittable) return;

            var position = Player.Instance.transform.position;
            var _direction = (transform.position - position );
            var blood = Instantiate(bloodPrefab, transform.position , Quaternion.identity);
            _direction = new Vector3(_direction.y, _direction.x, _direction.z); 
            blood.transform.rotation = Quaternion.FromToRotation(position, _direction);
            
            characterInfo.Health.ChangeCurrentBy(-damage);
            animationController.PlayHit();
            StartCoroutine(enemyController.Pushback());
            if (characterInfo.Health.ZeroOrLess)
                Kill();
        }
        public void CacheComponents() {
            animationController = GetComponent<EnemyAnimationController>();
            enemyController = GetComponent<EnemyController>();
        }
    }
}
