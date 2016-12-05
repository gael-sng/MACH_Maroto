using UnityEngine;
using System.Collections;

public class spawnerBehavior : Utilities {
	[Header("Current player reference")]
	public GameObject player;
	[Header("Enemies prfabs:")]
	public GameObject enemy_1;
	public GameObject enemy_2;
	public GameObject enemy_3;
	public GameObject enemy_4;

	public float spawnDelay;

	private float timer;

	// Use this for initialization
	void Start () {
		if (spawnDelay == 0)
			spawnDelay = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) return;
		if (timer >= spawnDelay) {
			timer = 0;
			GameObject enemy;
			switch((int)Random.Range(1, 5)){
			case 1:
				//print ("case 1");
				if (enemy_1) {
					enemy = Instantiate (enemy_1);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<EnemyControl>().player = player.transform;
					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()),
																				GetMaxVerticalPosition () + 1, 0);
				}
				break;

			case 2:
				//print ("case 2");
				if (enemy_2) {
					enemy = Instantiate (enemy_2);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<CampelBehaviour>().player = player.transform;
					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()),
						GetMaxVerticalPosition () + 1, 0);
				}
				break;

			case 3:
				//print ("case 3");
				if (enemy_3) {
					enemy = Instantiate (enemy_3);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<TarkelBehaviour>().player = player.transform;
					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()),
						GetMaxVerticalPosition () + 1, 0);
				}
				break;

			case 4:
				//print ("case 4");
				if (enemy_1) {
					enemy = Instantiate (enemy_1);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<EnemyControl>().player = player.transform;
					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()),
						GetMaxVerticalPosition () + 1, 0);
				}
				break;

			default:
				break;

			}
		}
		timer += Time.deltaTime;
	}
}
