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
	private int bossCounter;
	private int tarkelCounter;
	private PlayerControl PC;

	private int rank;

	// Use this for initialization
	void Start () {
		rank = 0;
		bossCounter = 1;
		tarkelCounter = 1;
		PC = player.GetComponent<PlayerControl> ();
		isBossTime = false;
		boss = null;
		if (spawnDelay == 0)
			spawnDelay = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) return;

		//rank de dificuldade sera calculado baseado que o nivel 1 o player tera matado cerca d 20 inimigos em 60 segundos
		rank = (int)Mathf.Floor((PC.getTimeAlive () * PC.getKillsCount ())/(1200.0f));
		print ("rank =" + rank);

		//verificandos se o boss ja acabou
		if (boss != null)
			return;
		else if (isBossTime) {
			isBossTime = false;
			Camera.main.GetComponent<PlayMusic> ().changeMusic();
		}

		//vendo se é a hora de começar spawnar o boss
		if (Harbingel && PC.getTimeAlive() >= (60.0f * bossCounter)) {
			print ("BOSS TIME");
			bossCounter++;
			Camera.main.GetComponent<PlayMusic> ().changeMusic ();

			boss = Instantiate (Harbingel);
			//Give to enemy a reference to the palyer
			boss.GetComponent<EnemyControl> ().player = player.transform;
			boss.GetComponent<EnemyControl> ().hitPoints = 12 * bossCounter;
			boss.GetComponent<BossBehaviour> ().player = player;
			//put the enemy in a random position above the screen
			boss.GetComponent<Transform> ().position = new Vector3();
			isBossTime = true;
		}


		if (Tarkel && PC.getTimeAlive() >= tarkelCounter * (29.0f / (rank + 1))) {
			tarkelCounter++;
			GameObject enemy;
			enemy = Instantiate (Tarkel);

			//Give to enemy a reference to the palyer
			enemy.GetComponent<TarkelBehaviour>().player = player;
			enemy.GetComponent<EnemyControl>().player = player.transform;
			enemy.GetComponent<EnemyControl> ().hitPoints = 4 * (rank + 1);

			//put the enemy in a random position above the screen
			enemy.GetComponent<Transform> ().position = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()), GetMaxVerticalPosition () + 0.5f, 0);;
		}

		Vector3 newPosition = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()), GetMaxVerticalPosition () + 0.5f, 0);
		if (timer >= spawnDelay) {
			timer = 0;
			GameObject enemy;
			print ("bora spawnar");
			switch((int)Random.Range(1, 3)){
			case 1:
				//print ("case 1");
				if (Stalkel) {
					enemy = Instantiate (Stalkel);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<EnemyControl>().player = player.transform;
					enemy.GetComponent<EnemyControl>().hitPoints = (float)rank + 1;
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
					enemy.GetComponent<CampelBehaviour> ().hitPoints = Mathf.Ceil((rank)/2);
					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = newPosition;
				}
				break;

			default:
				break;

			}
		}
		timer += Time.deltaTime;
	}
}
