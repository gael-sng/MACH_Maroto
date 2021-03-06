using UnityEngine;
using System.Collections;

public class gatlingBarrel : Utilities {
	public GameObject player;
	public float shootingDuration;
	public float shootingDelay;
	private float timer;
	public EnemyShooting script;
	// Use this for initialization
	void Start () {
		if (shootingDelay < 0)
			shootingDelay = 5;
		if (shootingDuration < 0)
			shootingDuration = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= shootingDelay) {
			script.enabled = true;
			if (timer >= shootingDelay + shootingDuration) {
				timer = 0;
				script.enabled = false;
			}
		}
		timer += Time.deltaTime;	
		


		if(player != null){
			Vector3 targetDir = player.transform.position - transform.position;
			float angle = RadianToDegree(Mathf.Atan(targetDir.y / targetDir.x));
			if (targetDir.x < 0)
				angle += 180;
			transform.rotation = Quaternion.Euler (0, 0, angle - 90.0f);

		}

	}
}
