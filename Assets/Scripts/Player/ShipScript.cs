﻿using UnityEngine;
using System.Collections;

//Basic ship functionality: hitpoints, speed, explosion, movemente. Used by player and enemies
public class ShipScript : Utilities {

    [Header("Hitpoints")]
    public float hitPoints;

    [Header("Max move speed")]
    public float speed;

	[Header("Explosion prefab")]
	public GameObject explosion;

    public static readonly float yFlipCoef = 45.0f;
    public static readonly float xFlipCoef = 15.0f;


    protected void TakeDamage(float damage) {
        hitPoints -= damage;

        if (hitPoints <= 0.0f) {
            DestroyShip();
        }
    }

    public virtual void MoveShip(Vector3 dir) { }

    // Use this for initialization
    public virtual void DestroyShip() {
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }
}
