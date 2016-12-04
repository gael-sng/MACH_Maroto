using UnityEngine;
using System.Collections;

public class CampelBehaviour : ShipScript {

	private float timer;
	[Header("Delay between move of a location to another")]
	public float movementDelay;
	private Vector3 dir;
	[Header("Current player reference")]
	public Transform player;
	private Transform enemy;

	[Header("Chance of dropping an Upgrade (Between 0 and 1)")]
	public float upgradeDropChance;

	[Header("Upgrade Sprite Prefab")]
	public GameObject upgradePrefab;

	private float randomX, randomY;


	// Use this for initialization
	void Start () {
		//player = GameObject.Find("Player").GetComponent<Transform>();
		enemy = this.gameObject.GetComponent<Transform> ();
		if(movementDelay <= 0)movementDelay = 10;
		timer = movementDelay;
	}

	// Update is called once per frame
	void Update () {
		if (timer >= movementDelay) {
			timer = 0;
			//chose a screem random position to move
			randomX = Random.Range (GetMinHorizontalPosition(), GetMaxHorizontalPosition());
			randomY = Random.Range (3.0f + GetMinVerticalPosition(), GetMaxVerticalPosition());
		}
		timer += Time.deltaTime;
		if(!(modulo(enemy.position.x - randomX) < 0.1f && modulo(enemy.position.y - randomY) < 0.1f))
			move(new Vector3(randomX - enemy.position.x, randomY - enemy.position.y, 0));

		//point the barrel to the player, by rotating the enemy
		if(player != null){
			Vector3 targetDir = player.position - transform.position;
			float angle = RadianToDegree(Mathf.Atan(targetDir.y / targetDir.x));
			if (targetDir.x < 0)
				angle += 180;
			transform.rotation = Quaternion.Euler (0, 0, angle - 90.0f);
			ClearConsole ();
			print ("angle = " + angle + " x = " + targetDir.x + " y = " + targetDir.y + " z = " + targetDir.z);
		}
	}



	void move(Vector3 newPosition) {
			newPosition = transform.position + newPosition * Time.deltaTime * speed;
			transform.position = new Vector3(Mathf.Clamp(newPosition.x, GetMinHorizontalPosition(), GetMaxHorizontalPosition()),
				Mathf.Clamp(newPosition.y, GetMinVerticalPosition() - 3.0f, GetMaxVerticalPosition() + 1.0f), 0);
	}

	public override void DestroyShip() {
		//Before destroing, I should add a piece of code to kill enemy

		//It has a chance of dropping an Upgrade, which depends on the level of this enemy
		if (Random.value < upgradeDropChance && upgradePrefab != null) {
			Instantiate(upgradePrefab, transform.position, Quaternion.identity);
		}

		base.DestroyShip(); //Do the DestroyShip stuff
	}

	void OnTriggerEnter (Collider col) {
		print ("ACERTO MISERAVI");
		if (col.gameObject.tag == "PlayerBullet") {
			TakeDamage(col.gameObject.GetComponent<bulletScript>().GetDamage());
			col.gameObject.SendMessage("Destroy");
		} else if (col.gameObject.tag == "Player") {
			TakeDamage(hitPoints);
		}
	}

	private float modulo(float n){
		if (n < 0)
			return - n;
		return n;
	}
}
