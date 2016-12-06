using UnityEngine;
using System.Collections;

public class MissileBehaviour : Utilities {
	public GameObject target;
	public float hitPoints;
	public float maxSpeed;
	public float acceleration;
	private float speed;
	private float rotationSpeed;
	public GameObject explosion;
    public float damage=2;
	// Use this for initialization
	void Start () {
		speed = 0;
		if (maxSpeed == 0)
			maxSpeed = 3;
		if (acceleration == 0)
			acceleration = maxSpeed;
		rotationSpeed = maxSpeed / 3;
	}
	
	// Update is called once per frame
	/*void Update () {
		//transform.Translate(Vector3.up * speed * Time.deltaTime);
		if(target != null){
			Vector3 targetDir = target.transform.position - transform.position;
			float angle = RadianToDegree(Mathf.Atan(targetDir.y / targetDir.x));
			if (targetDir.x < 0)
				angle += 180;
			//transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.Euler (0, 0, (angle * speed * Time.deltaTime) - 90.0f), 180);
			transform.rotation = Quaternion.Euler (0, 0, (angle * speed * Time.deltaTime) - 90.0f);

		}
	}*/
	void Update() {
		//verificando se o missel esta fora da tela para explodilo
		float x = gameObject.transform.position.x, y = gameObject.transform.position.y;
		if (x < GetMinHorizontalPosition() - 1 || x > GetMaxHorizontalPosition() + 1 || y < GetMinVerticalPosition() - 1)
			Destroy ();
		if (target == null)
			return;
		//rotacionando o missel em direção ao target
		Vector3 targetDir = target.transform.position - transform.position;
		float step = rotationSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
		//acelerando
		speed = Mathf.Clamp(speed + acceleration * Time.deltaTime, 0, maxSpeed);
		//movimentando o missel apra frente
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		
	}
	public void Destroy(){
		Instantiate(explosion, this.transform.position, this.transform.rotation);
		Destroy(gameObject);
	}

	private void TakeDamage(float damage) {
		hitPoints -= damage;

		if (hitPoints <= 0.0f) {
			Destroy();
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == (target.tag + "Bullet")) {
			TakeDamage (col.gameObject.GetComponent<bulletScript> ().GetDamage ());
		}
	}

    public float GetDamage()
    {
        return damage;
    }
}
