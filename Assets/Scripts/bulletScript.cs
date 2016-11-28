/*using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

    private float speed;
    private float bulletDamage;

    // Use this for initialization
    void Start () {
        Destroy(transform.gameObject, 3.0f);

		SetColor(Color.yellow);
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

    public void SetColor (Color color) {

        TrailRenderer trail = GetComponent<TrailRenderer>();
        Material newMaterial = new Material(trail.material);
        newMaterial.SetColor("_TintColor", color);

        trail.material = newMaterial;

        Light light = GetComponent<Light>();
        light.color = color;
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
*/
using UnityEngine;
using System.Collections;

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