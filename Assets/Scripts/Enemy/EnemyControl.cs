using UnityEngine;
using System.Collections;

public class EnemyControl: ShipScript {

	private float timer;
	public float movementDelay;
	private int dir;

	public Transform player;
	public float verticalSpeed; 
	public float maxHorizontalSpeed;
	public float aceleration;
	private float horizontalSpeed;
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
		if (maxHorizontalSpeed < 0)
			maxHorizontalSpeed = 3;
		if(movementDelay <= 0)movementDelay = 1;
		aceleration *= maxHorizontalSpeed; 
    }

    // Update is called once per frame
    void Update () {
		if (timer >= movementDelay && player != null) {
			timer = 0;
			if (modulo (player.position.x - enemy.position.x) < 0.3) {
				if (modulo (horizontalSpeed) > 0.1)horizontalSpeed = 0;
				else if(horizontalSpeed < 0)horizontalSpeed += aceleration * Time.deltaTime*2;			
				else if (horizontalSpeed > 0)horizontalSpeed -= aceleration * Time.deltaTime*2;
			} else if (player.position.x > enemy.position.x)
				horizontalSpeed = Mathf.Clamp (horizontalSpeed + aceleration * Time.deltaTime, -maxHorizontalSpeed, maxHorizontalSpeed);
			else
				horizontalSpeed = Mathf.Clamp (horizontalSpeed - aceleration * Time.deltaTime, -maxHorizontalSpeed, maxHorizontalSpeed);
		}
		timer += Time.deltaTime;

        move();

        //Needs to destroy object when its out of camera range
	}
    
    void move() {
		Vector3 newPosition = transform.position + new Vector3(horizontalSpeed, -verticalSpeed, 0) * Time.deltaTime;

		transform.position = new Vector3(Mathf.Clamp(newPosition.x, GetMinHorizontalPosition(), GetMaxHorizontalPosition()),
										Mathf.Clamp(newPosition.y, GetMinVerticalPosition() - 3.0f, GetMaxVerticalPosition() + 1), 0);

		transform.eulerAngles = defaultAngle + new Vector3(0,(-horizontalSpeed/maxHorizontalSpeed)*  yFlipCoef, 0.0f);
        
	}

    public override void DestroyShip() {
        //Before destroing, I should add a piece of code to kill enemy

        //It has a chance of dropping an Upgrade, which depends on the level of this enemy
		if (Random.value < upgradeDropChance && upgradePrefab != null) {
            Instantiate(upgradePrefab, transform.position, Quaternion.identity);
        }
        GameObject.Find("player").GetComponent<PlayerControl>().KillConfirmed();
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
