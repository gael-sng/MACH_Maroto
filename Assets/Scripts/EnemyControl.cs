using UnityEngine;
using System.Collections;

public class EnemyControl: ShipScript {

    [Header("Chance of dropping an Upgrade (Between 0 and 1)")]
    public float upgradeDropChance;

    [Header("Upgrade Sprite Prefab")]
    public GameObject upgradePrefab;

    private Vector2 dir;

    private Vector3 defaultAngle;

    void Start() {
        defaultAngle = transform.eulerAngles;
        dir = new Vector2(0.0f, -1.0f);
    }

    // Update is called once per frame
    void Update () {
        MoveShip(dir);

        //Needs to destroy object when its out of camera range
	}
    
    public override void MoveShip(Vector3 dir) {
        transform.position += new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;

        if (GetComponent<Collider>().enabled)
            transform.eulerAngles = defaultAngle + new Vector3(0, -dir.x * yFlipCoef, dir.y * xFlipCoef);
    }

    void OnBecameVisible() {
        GetComponent<MeshCollider>().enabled = true;
    }

    public override void DestroyShip() {
        //Before destroing, I should add a piece of code to kill enemy

        //It has a chance of dropping an Upgrade, which depends on the level of this enemy
        if (Random.value < upgradeDropChance) {

            PlayerControl player = GameObject.Find("Player").GetComponent<PlayerControl>();

            Instantiate(upgradePrefab, transform.position, Quaternion.identity);
        }

        base.DestroyShip(); //Do the DestroyShip stuff
    }

    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "PlayerBullet") {
            TakeDamage(col.gameObject.GetComponent<bulletScript>().GetDamage());
            col.gameObject.SendMessage("Destroy");
        } else if (col.gameObject.tag == "Player") {
            TakeDamage(hitPoints);
        }
    }
}
