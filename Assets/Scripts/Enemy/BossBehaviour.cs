using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {
	public GameObject player;

	public float gatlingDelay;
	public GameObject B1;
	public GameObject B2;

	public float missileDelay;
	public missileLauncherBehaviour ML1;
	public missileLauncherBehaviour ML2;
	public missileLauncherBehaviour ML3;
	public missileLauncherBehaviour ML4;

	public Transform MB;//mega barrel
	public GameObject MegaBullet;
	private GameObject saveMB;

	public EnemyControl myControl;

	private float timer;
	private float wait;
	private bool isShooting;
	// Use this for initialization
	void Start () {
		timer = 0;
		isShooting = false;
		B1.GetComponent<gatlingBarrel>().player = player;
		B2.GetComponent<gatlingBarrel>().player = player;
		ML1.target = player;
		ML2.target = player;
		ML3.target = player;
		ML4.target = player;

		wait = ML1.delay;
		ML1.delay = 4 * wait;
		ML2.delay = 4 * wait;
		ML3.delay = 4 * wait;
		ML4.delay = 4 * wait;

		saveMB = null;
	}

	// Update is called once per frame
	void Update () {
		if (!isShooting) {
			if (timer > 1 * wait)
				ML1.enabled = true;
			if (timer > 2 * wait)
				ML2.enabled = true;
			if (timer > 3 * wait)
				ML3.enabled = true;
			if (timer > 4 * wait)
				ML4.enabled = true;

			if (timer > 2 * wait)
				B1.GetComponent<gatlingBarrel> ().enabled = true;
			if (timer > 4 * wait) {
				B2.GetComponent<gatlingBarrel> ().enabled = true;
				isShooting = true;
			}
		}
		//shoot mega barrel
		if (timer > 6 * wait && saveMB == null) {
			stop ();
			Quaternion bulletRotation = Quaternion.Euler(Vector3.forward*MB.transform.eulerAngles.z);
			saveMB = (GameObject)GameObject.Instantiate (MegaBullet, MB.position, bulletRotation);
			saveMB.GetComponent<bulletScript> ().SetDamage(100);
		}
		if (timer > 6 * wait + 1) {
			saveMB.GetComponent<bulletScript> ().SetSpeed (20);
			timer = 0;
		}
		timer += Time.deltaTime;
	}



	private void stop(){
		isShooting = false;
		ML1.enabled = false;
		ML2.enabled = false;
		ML3.enabled = false;
		ML4.enabled = false;

		B1.GetComponent<gatlingBarrel>().enabled = false;
		B2.GetComponent<gatlingBarrel>().enabled = false;
	}
}
