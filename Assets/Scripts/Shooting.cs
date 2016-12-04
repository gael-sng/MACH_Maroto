using UnityEngine;
using System.Collections;

public abstract class Shooting : MonoBehaviour {

    protected float timer = 0.0f;

    protected float bulletDamage;
    protected float shootingInterval;
    protected float bulletSpeed;
    protected GameObject bulletPrefab;

    // Update is called once per frame
    void Update() {

        if (timer >= shootingInterval) {
            timer = 0;
            Shoot();
        }
        timer += Time.deltaTime;


    }

    protected abstract void Shoot();

    protected void ShootFromBarrel(GameObject barrel) {
        Quaternion bulletRotation = Quaternion.Euler(Vector3.forward*barrel.transform.eulerAngles.z);
        GameObject bulletInst = (GameObject) Instantiate(bulletPrefab, barrel.transform.position, bulletRotation);

        bulletInst.GetComponent<bulletScript>().SetSpeed(bulletSpeed);
        bulletInst.GetComponent<bulletScript>().SetDamage(bulletDamage);
        bulletInst.GetComponent<bulletScript>().SetTag(gameObject.tag);
    }
}
