using UnityEngine;
using System.Collections;

public class TarkelBehaviour : Utilities {
	public GameObject player;
	public gatlingBarrel GB;
	public missileLauncherBehaviour ML1;
	public missileLauncherBehaviour ML2;

	private float wait;
	private float timer;
	// Use this for initialization
	void Start () {
		timer = 0;
		//intercalando os tiros dos missile lauchers
		wait = ML1.delay;
		ML1.delay = 2 * wait;
		ML2.delay = 2 * wait;

		GB.player = player;
		ML1.target = player;
		ML1.enabled = false;
		ML2.target = player;
		ML2.enabled = false;
		}
	
	// Update is called once per frame
	void Update () {
		if (timer > wait) ML1.enabled = true;
		if (timer > 2*wait) ML2.enabled = true;
		timer += Time.deltaTime;

	}
}
