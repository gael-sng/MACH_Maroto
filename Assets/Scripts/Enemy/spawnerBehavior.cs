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
	private float tarkelFreq;
	private float tarkelTimer;
	private PlayerControl PC;

	private float rank;

	// Use this for initialization
	void Start () {
		tarkelTimer = 0;
		bossCounter = 1;
		tarkelFreq = 40;
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
		rank = (0.3f + 0.7f*ScoreSystem.GetUserRankCoeficient())* (1.0f + PC.getTimeAlive () / 75.0f);
        spawnDelay = Mathf.Clamp(5.0f - 1.2f*rank, 2.0f, 5.0f);

        print("Rank: " + rank + " Delay: " + spawnDelay + " - PlayerRankCoef: " + ScoreSystem.GetUserRankCoeficient());

        //verificandos se o boss ja acabou
        if (boss != null)
			return;
		else if (isBossTime) {
			isBossTime = false;
			Camera.main.GetComponent<PlayMusic> ().changeMusic();
		}
		tarkelTimer += Time.deltaTime;
		//isso far ao tarkel ser spwanado no primeiro nivel 1 vez, segundo nivel 2 veses assim por diante
		if (Tarkel && tarkelTimer >= tarkelFreq) {
			tarkelFreq *=0.90f;
			tarkelTimer = 0;
			GameObject enemy;
			enemy = Instantiate (Tarkel);

			//Give to enemy a reference to the palyer
			enemy.GetComponent<TarkelBehaviour>().player = player;
			enemy.GetComponent<EnemyControl>().player = player.transform;
			enemy.GetComponent<EnemyControl> ().hitPoints = Mathf.FloorToInt(Mathf.Pow(rank, 1.5f) * 3);

			//put the enemy in a random position above the screen
			enemy.GetComponent<Transform> ().position = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()), GetMaxVerticalPosition () + 0.2f, 0);;
		}
			
		//vendo se é a hora de começar spawnar o boss
		if (Harbingel && PC.getTimeAlive() >= (60.0f * bossCounter)) {
			print ("BOSS TIME");
			bossCounter++;
			Camera.main.GetComponent<PlayMusic> ().changeMusic ();

			boss = Instantiate (Harbingel);
			//Give to enemy a reference to the palyer
			boss.GetComponent<EnemyControl> ().player = player.transform;
			boss.GetComponent<EnemyControl> ().hitPoints = Mathf.FloorToInt(6 * Mathf.Pow(rank, 1.5f) * bossCounter);
			boss.GetComponent<BossBehaviour> ().player = player;
			//put the enemy in a random position above the screen
			boss.GetComponent<Transform> ().position = new Vector3(0, GetMaxVerticalPosition()+0.5f, 0);
			isBossTime = true;
		}

        int max_inimigos = 3 + (int)Mathf.Floor(rank);

        Vector3 newPosition = new Vector3 (Random.Range (GetMinHorizontalPosition (), GetMaxHorizontalPosition()), GetMaxVerticalPosition () + 0.1f, 0);
		if (timer >= spawnDelay && (GameObject.FindGameObjectsWithTag("Enemy").Length < max_inimigos)) {
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
					enemy.GetComponent<EnemyControl>().hitPoints =1 + Mathf.FloorToInt(Mathf.Pow(rank, 1.5f));
					enemy.GetComponent<EnemyControl> ().verticalSpeed = 1.0f + rank * 0.2f;
					enemy.GetComponent<EnemyControl> ().maxHorizontalSpeed = 1.5f + rank * 0.4f;
					enemy.GetComponent<EnemyControl> ().movementDelay = 0.1f + (1.0f/rank);

                    enemy.GetComponent<EnemyShooting>().ShootingInterval = 0.4f + (1.0f / rank) * 0.45f;

                    //put the enemy in a random position above the screen
                    enemy.GetComponent<Transform> ().position = newPosition;
				}
				break;

			case 2:
				//print ("case 2");
				if (Campel) {
					enemy = Instantiate (Campel);

					//Give to enemy a reference to the palyer
					enemy.GetComponent<CampelBehaviour>().player = player.transform;
                    enemy.GetComponent<CampelBehaviour>().hitPoints = 1 + Mathf.FloorToInt(Mathf.Pow(rank, 1.5f));
                    enemy.GetComponent<CampelBehaviour>().movementDelay = 4.0f + (6.0f / rank);

                    enemy.GetComponent<EnemyShooting>().ShootingInterval = 0.5f + (1.0f / rank)*0.5f;

					//put the enemy in a random position above the screen
					enemy.GetComponent<Transform> ().position = newPosition;
				}
				break;

			default:
				break;

			}
		}

        if (PC.getTimeAlive() >= (60.0f * bossCounter) - 4.0f) {
            timer = 0.0f;
        } else {
            timer += Time.deltaTime;
        }
	}
}
