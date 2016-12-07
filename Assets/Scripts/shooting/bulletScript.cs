
using UnityEngine;
using System.Collections;

//Main bullet script. Moves bullet, stores its damage value and destroys it when needed
public class bulletScript : MonoBehaviour {

	private float speed;
	private float bulletDamage;

	// Use this for initialization
	void Start () {
		Destroy(transform.gameObject, 3.0f);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}

	public void SetSpeed(float speed) {
		this.speed = speed;
	}

	public void SetTag(string tag) {
		gameObject.tag = tag + "Bullet";
	}

	public float GetDamage() {
		return bulletDamage;
	}

	public void SetDamage(float bulletDamage) {
		this.bulletDamage = bulletDamage;
	}

	public void Destroy() {
		Destroy(gameObject);
	}
}