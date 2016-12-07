using UnityEngine;
using System.Collections;

//Basic shooting script for enemies
public class EnemyShooting : Shooting {

    [Header("Bullet damage")]
    public float BulletDamage;
    [Header("Secs per shot")]
    public float ShootingInterval;
    [Header("Bullet speed")]
    public float BulletSpeed;
    [Header("Bullet Prefab")]
    public GameObject BulletPrefab;

    private GameObject barrel;

    void Start() {
        bulletDamage = BulletDamage;
        shootingInterval = ShootingInterval;
        bulletSpeed = BulletSpeed;
        bulletPrefab = BulletPrefab;

        foreach (Transform child in transform)
            if (child.tag == "Barrel")
                barrel = child.gameObject;
    }

    protected override void Shoot() {
        ShootFromBarrel(barrel);
    }
}