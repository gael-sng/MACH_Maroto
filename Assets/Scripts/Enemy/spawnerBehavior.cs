using UnityEngine;
using System.Collections;

public class spawnerBehavior : Utilities {
	[Header("Current player reference")]
	public GameObject player;
	[Header("Enemies prfabs:")]
	public GameObject Stalkel;
	public GameObject Campel;
	public GameObject Tarkel;
	public GameObject Harbingel;

	public float spawnDelay;

	//public PlayMusic audio;

	private float timer;

	private GameObject boss;
	private bool isBossTime;

	// Use this for initialization
	void Start () {
		isBossTime = false;
		boss = null;
		if (spawnDelay == 0)
			spawnDelay = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) return;
		if (boss != null)
			return;
		else if (isBossTime) {
			isBossTime = false;
			Camera.main.GetComponent<PlayMusic> ().changeMusic();
		}
		
		Vector3 newPosition = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()), GetMaxVerticalPosition () + 0.5f, 0);
		if (timer >= spawnDelay) {
			timer = 0;
			GameObject enemy;
			print ("bora spawnar");
			switch((int)Random.Range(1, 5)){
			case 1:
				//print ("case 1");
				if (Stalkel) {
					enemy = Instantiate (Stalkel);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<EnemyControl>().player = player.transform;
					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = newPosition;
				}
				break;

			case 2:
				//print ("case 2");
				if (Campel) {
					enemy = Instantiate (Campel);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<CampelBehaviour> ().player = player.transform;
					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = newPosition;
				}
				break;

			case 3:
				//print ("case 3");
				if (Tarkel) {
					enemy = Instantiate (Tarkel);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<TarkelBehaviour>().player = player;
					enemy.GetComponent<EnemyControl>().player = player.transform;

					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = newPosition;
				}
				break;

			case 4:
				//print ("case 4");
				if (Harbingel) {
					print ("BOSS TIME");
					Camera.main.GetComponent<PlayMusic> ().changeMusic ();

					enemy = Instantiate (Harbingel);
					boss = enemy;
					//Give to enemy a reference to the palyer
					enemy.GetComponent<EnemyControl>().player = player.transform;
					enemy.GetComponent<BossBehaviour>().player = player;
					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = newPosition;
					isBossTime = true;
				}
				break;

			default:
				break;

			}
		}
		timer += Time.deltaTime;
	}
}
