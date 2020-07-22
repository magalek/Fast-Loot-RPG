using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Entities;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Projectile : MonoBehaviour {
    public event Action Destroyed;

    public event Action ShouldDestroy;
    
    private Vector3 targetDirection;
    private Cooldown<Projectile> cooldown;
    
    public void Start() {
        cooldown = new Cooldown<Projectile>(this, 3, p => p.ShouldDestroy?.Invoke());
        cooldown.Start();
        
        ShouldDestroy += DestroyProjectile;
    }

    private void Update() {
        transform.position += targetDirection * Time.deltaTime;
    }

    public void SetTarget(Vector3 targetPosition) {
        targetDirection = targetPosition - transform.position;
        targetDirection = targetDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<IHittable>().Hit(10);
            ShouldDestroy?.Invoke();
        }
        if (!other.CompareTag("Enemy") && !other.CompareTag("Player") && !other.CompareTag("Item") && !other.CompareTag("Weapon")) {
            ShouldDestroy?.Invoke();
        }
    }

    private void DestroyProjectile() {
        ShouldDestroy -= DestroyProjectile;
        Destroy(gameObject);
    }
    private void OnDestroy() {
        Destroyed?.Invoke();
    }
}

