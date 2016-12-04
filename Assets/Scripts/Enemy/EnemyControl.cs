﻿using UnityEngine;
using System.Collections;

public class EnemyControl: ShipScript {

	private float timer;
	public float movementDelay;
	private int dir;

	public Transform player;
	public float verticalSpeed; 
	public float horizontalSpeed;
	private Transform enemy;

    private Vector3 defaultAngle;


    [Header("Chance of dropping an Upgrade (Between 0 and 1)")]
    public float upgradeDropChance;

    [Header("Upgrade Sprite Prefab")]
    public GameObject upgradePrefab;


    // Use this for initialization
    void Start () {
        //player = GameObject.Find("Player").GetComponent<Transform>();
        defaultAngle = transform.eulerAngles;
        enemy = this.gameObject.GetComponent<Transform> ();
		timer = 0;
		dir = 0;
		if(movementDelay <= 0)movementDelay = 1;
    }

    // Update is called once per frame
    void Update () {
		if (timer >= movementDelay) {
			timer = 0;
			if (modulo (player.position.x - enemy.position.x) < 0.1)
				dir = 0;
			else if (player.position.x > enemy.position.x)
				dir = 1;
			else
				dir = -1;
		}
		timer += Time.deltaTime;

        move();

        //Needs to destroy object when its out of camera range
	}
    
    void move() {
		//dont move
		if (dir == 0)
			return;
		else {
	
		Vector3 newPosition;
		//to the right
		if(dir ==  1)newPosition = transform.position + new Vector3(horizontalSpeed, -verticalSpeed, 0) * Time.deltaTime;
		//to the left
		else newPosition = transform.position + new Vector3(-horizontalSpeed, -verticalSpeed, 0) * Time.deltaTime;

		transform.position = new Vector3(Mathf.Clamp(newPosition.x, GetMinHorizontalPosition(), GetMaxHorizontalPosition()),
										Mathf.Clamp(newPosition.y, GetMinVerticalPosition() - 3.0f, GetMaxVerticalPosition() + 1), 0);

        transform.eulerAngles = defaultAngle + new Vector3(0, -dir* yFlipCoef, 0.0f);
        }
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