using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    [Header("Bullets prefab")]
    public bulletType[] bulletTypes;

    [Header("Gun barrel")]
    public GameObject barrel;


    [System.Serializable]
    public struct bulletType {
        [Header("Secs per shot")]
        public float shootingInterval;
        [Header("Bullet Prefab")]
        public GameObject bulletPrefab;
    }
    private bulletType actualBullet;
    private float timer = 0.0f;
    // Use this for initialization
    void Start () {
        actualBullet = bulletTypes[0];
    }
	
	// Update is called once per frame
	void Update () {
        
        if (timer >= actualBullet.shootingInterval) {
            timer = 0;
            Shoot();
        }
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            UpgradeBullet();
	}

    private void UpgradeBullet() {
        for (int i=0; i< bulletTypes.Length; i++) {
            if (bulletTypes[i].Equals(actualBullet)) { 
                if (i+1 < bulletTypes.Length) {
                    actualBullet = bulletTypes[i + 1];
                } else {
                    //Already has the last bullet type
                }
            }
        }
    }

    private void Shoot() {
        Instantiate(actualBullet.bulletPrefab, barrel.transform.position, barrel.transform.rotation);
    }

}
