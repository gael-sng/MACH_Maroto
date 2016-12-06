using UnityEngine;
using System.Collections;

public class PlayerShooting : Shooting {

    public bulletType[] bulletTypes;

    [System.Serializable]
    public struct bulletType {
        [Header("Bullet damage")]
        public float bulletDamage;
        [Header("Secs per shot")]
        public float shootingInterval;
        [Header("Bullets per shot (min:1 - max:5)")]
        public int shootingCount;
        [Header("Bullet speed")]
        public float bulletSpeed;
        [Header("Bullet Prefab")]
        public GameObject bulletPrefab;
    }
    private int actualBulletIdx;
    private GameObject[] barrels;
    private Quaternion barrelDefaultAngle;

    private enum barrelIDs { LEFT_BARREL, MIDDLE_LEFT_BARREL, MIDDLE_BARREL, MIDDLE_RIGHT_BARREL, RIGHT_BARREL, barrelCount };

    // Use this for initialization
    void Start() {
        SetBulletType(0);

        barrels = new GameObject[(int)barrelIDs.barrelCount];
        int count = 0;
        foreach (Transform child in transform)
            if (child.tag == "Barrel")
                barrels[count++] = child.gameObject;
    }

    protected override void Shoot() {
        switch(bulletTypes[actualBulletIdx].shootingCount) {
            case 1:
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_BARREL]);
                break;
            case 2:
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_RIGHT_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_LEFT_BARREL]);
                break;

            case 3:
                ShootFromBarrel(barrels[(int)barrelIDs.RIGHT_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.LEFT_BARREL]);
                break;

            case 4:
                ShootFromBarrel(barrels[(int)barrelIDs.RIGHT_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_RIGHT_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_LEFT_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.LEFT_BARREL]);
                break;
            case 5:
                ShootFromBarrel(barrels[(int)barrelIDs.RIGHT_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_RIGHT_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.MIDDLE_LEFT_BARREL]);
                ShootFromBarrel(barrels[(int)barrelIDs.LEFT_BARREL]);
                break;
        }
    }

    private void SetBulletType(int newBulletIdx) {
        bulletType type = bulletTypes[newBulletIdx];
        bulletDamage = type.bulletDamage;
        shootingInterval = type.shootingInterval;
        bulletSpeed = type.bulletSpeed;
        bulletPrefab = type.bulletPrefab;
    }

    public void UpgradeBullet() {

        if (actualBulletIdx + 1 < bulletTypes.Length) {
            SetBulletType(++actualBulletIdx);
        } else {
            //Already has the last bullet type
        }
    }

    public GameObject[] GetBarrels() {
        return barrels;
    }
}
