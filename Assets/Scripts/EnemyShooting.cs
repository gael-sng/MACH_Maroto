using UnityEngine;
using System.Collections;

public class EnemyShooting : Shooting {

    [Header("Bullet damage")]
    public new float bulletDamage;
    [Header("Secs per shot")]
    public new float shootingInterval;
    [Header("Bullet speed")]
    public new float bulletSpeed;
    [Header("Bullet Prefab")]
    public new GameObject bulletPrefab;

    private GameObject barrel;

    void Start() {
        foreach (Transform child in transform)
            if (child.tag == "Barrel")
                barrel = child.gameObject;
    }

    protected override void Shoot() {
        ShootFromBarrel(barrel);
    }
}