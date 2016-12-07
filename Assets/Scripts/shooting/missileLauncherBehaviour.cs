using UnityEngine;
using System.Collections;

//Behaviour for a ship that launches missiles 
public class missileLauncherBehaviour : Utilities {
	public GameObject target;
	public GameObject missilePrefab;
	public float delay;

	private float timer;
	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= delay && missilePrefab && target) {
			timer = 0;
			shoot();
		}
		timer += Time.deltaTime;

	}


	public void shoot(){
		Quaternion missileRotation = Quaternion.Euler (90, 0, 0);;
		GameObject missile = (GameObject) GameObject.Instantiate (missilePrefab, transform.position, missileRotation);
		missile.GetComponent<MissileBehaviour> ().target = target;
		// Coloque os parametros que quiser no missel
		//missile.GetComponent<MissileBehaviour> ().maxSpeed = 3.0f;
	}
}
