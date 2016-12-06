using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : ShipScript {
    
    [Header("Max vertical position between 0(bot) and 1(top)")]
    public float maxVerticalPosition;
    [Header("Life Sprite Prefab")]
    public GameObject life;
    [Header("Life Background Prefab")]
    public GameObject lifeaBackground;
    [Header("New Life Prefab")]
    public GameObject lifeUP;               //GameObject para imagem de LifeUP

    private Camera mainCamera;
    private float score;
    private bool alive;//, isInvunerable;
    private Vector3 positionVidas;
    private float aux;
    private GameObject[] countLives = new GameObject[10];
    private float countdown = 10.0f;        //Variavel para contagem de tempo para ganhar nova vida (decidir um tempo bom para ganhar novas vidas)
    private bool vidaSwitch = false;
    private GameObject lifeUPRunTime;
    private Vector3 defaultAngle;
    private Quaternion barrelDefaultAngle;
    private float timeAlive;
    private int killsCount;

    public static readonly int maxLifes = 10;

    public static readonly float SHIP_WIDTH = 0.7f;
    public static readonly float SHIP_HEIGHT = 0.7f;

	private bool flag;//gambiarra para funcionar, by cartaz blame him

    void Start() {
		flag = true;//gambiarra
        defaultAngle = transform.eulerAngles;
		//barrelDefaultAngle = null;// GetComponent<PlayerShooting>().GetBarrels()[0].transform.rotation;
        mainCamera = Camera.main;
        score = 0;
        alive = true;
        timeAlive = 0.0f;
        killsCount = 0;

        aux = (GetMaxHorizontalPosition() - GetMinHorizontalPosition()) / 12.0f;
		lifeaBackground.transform.localScale = new Vector3(GetMaxHorizontalPosition() - GetMinHorizontalPosition(), aux * 0.5f, 1);
        positionVidas = new Vector3(aux * 1.5f + GetMinHorizontalPosition(), -0.5f * aux + GetMaxVerticalPosition(), -1);

        for (int i = 0; i < maxLifes; i++)
        {
            countLives[i] = (GameObject)Instantiate(life, positionVidas, Quaternion.identity);
            countLives[i].SetActive(false);
			if (i == (int)(maxLifes / 2)) Instantiate (lifeaBackground, positionVidas + Vector3.forward * 0.5f, Quaternion.identity);
            positionVidas = positionVidas + Vector3.right * aux;
        }

        if (gameObject.GetComponent<PlayerControl>().hitPoints > maxLifes)
            gameObject.GetComponent<PlayerControl>().hitPoints = 10;

        for (int i = 0; i < gameObject.GetComponent<PlayerControl>().hitPoints; i++)
            countLives[i].SetActive(true);


    }
	
	// Update is called once per frame
	void Update () {
        //gambiarra apra funcioanr
        if (flag) {//gambiarra
			barrelDefaultAngle = GetComponent<PlayerShooting> ().GetBarrels () [0].transform.rotation;//gambiarra
			flag = false;//gambiarra
		}//gambiarra

        Vector2 dir;//dir.x between 0 and 1. Same for dir.y
        dir = InputControl.GetMoveDirection();
        countdown -= Time.deltaTime;		//Atualizacao do tempo

        if (!dir.Equals(Vector2.zero))
            MoveShip(dir);

        if (vidaSwitch)						//Destroi mensagem
            if (countdown <= 9.0f)
            {
                Destroy(lifeUPRunTime);
                vidaSwitch = false;
            }

        if (countdown <= 0.0f)				//quando chegar em zero ele ganha a vida
        {
            AddLive();
            countdown = 10.0f;
            vidaSwitch = true;
        }

        //Rotaciona os barrels
        foreach (GameObject b in GetComponent<PlayerShooting>().GetBarrels()) {
            b.transform.rotation = barrelDefaultAngle;
        }

        if (alive) {
            score += Time.deltaTime;
        }

        timeAlive += Time.deltaTime;

        //For testing:
        //GameObject.Find("Condition").GetComponent<TextMesh>().text = "Scr:" + score.ToString("F1") + "/Highscr:" + PersistentData.GetHighscore().ToString("F1") + "/Inv:" + (InvunerabilityCounter>=Invunerability_Charge_Time);
    }


    
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "EnemyBullet") {
            TakeDamage(col.gameObject.GetComponent<bulletScript>().GetDamage());
            col.gameObject.SendMessage("Destroy");
            RemoveLive();
        } else if (col.gameObject.tag == "Enemy") {
            TakeDamage(hitPoints);
            RemoveLive();
        }else if (col.gameObject.tag == "UP") {
            GetComponent<PlayerShooting>().UpgradeBullet();
            Destroy(col.gameObject);
        }
    }
    

    public override void DestroyShip() {

        base.DestroyShip(); //Do the DestroyShip stuff

        //After destroing, I should add a piece of code to kill player:

        alive = false;


        float matchScore = ScoreSystem.ReceiveMatchResults(timeAlive, killsCount);
        print("THIS MATCH SCORE WAS: " + matchScore);

        //Should call a coroutine to wait a few secs, and then shows pop-up menu to "Restart Game"
        //For now, it just reinitializes...
        SceneManager.LoadScene("Game Over");

    }

    public override void MoveShip(Vector3 dir) {
        Vector3 newPosition = transform.position + new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;

		newPosition = new Vector3(Mathf.Clamp(newPosition.x, GetPlayerMinHorizontalPosition(), GetPlayerMaxHorizontalPosition()),
			Mathf.Clamp(newPosition.y, GetPlayerMinVerticalPosition(), GetPlayerMaxVerticalPosition()), 0);

        if (GetComponent<Collider>().enabled)
        {
            transform.eulerAngles = defaultAngle + new Vector3(-dir.y * xFlipCoef, -dir.x * yFlipCoef, 0.0f);

        }
            

        transform.position = newPosition;

    }

    public void AddLive()
    {
        if (gameObject.GetComponent<PlayerControl>().hitPoints < 10)
        {
            
            lifeUPRunTime = (GameObject)Instantiate(lifeUP, new Vector3(transform.position.x+ SHIP_WIDTH, transform.position.y+SHIP_WIDTH, this.transform.position.z-1), Quaternion.identity); //Imagem de LIFEUP
            lifeUPRunTime.transform.localScale = new Vector3(0.05f, 0.05f, 1);             //Tamanho da imagem de aumento de vida
            lifeUPRunTime.transform.SetParent(transform);
            countLives[(int)gameObject.GetComponent<PlayerControl>().hitPoints].SetActive(true);
            gameObject.GetComponent<PlayerControl>().hitPoints++;
        }
    }
    public void RemoveLive()
    {
        countLives[(int)gameObject.GetComponent<PlayerControl>().hitPoints].SetActive(false);
    }

    public float GetPlayerMinHorizontalPosition() {
        return SHIP_WIDTH / 2.0f - mainCamera.orthographicSize * Screen.width / Screen.height;
    }
    public float GetPlayerMaxHorizontalPosition() {
        return mainCamera.orthographicSize * Screen.width / Screen.height - SHIP_WIDTH / 2.0f;
    }
    public float GetPlayerMinVerticalPosition() {
        return SHIP_HEIGHT / 2.0f + (Camera.main.ScreenToWorldPoint(Vector2.up * (GetComponent<InvunerabilityControl>().GetBarHeight())).y);
    }
	public float GetPlayerMaxVerticalPosition() {
        return (2*maxVerticalPosition-1) * mainCamera.orthographicSize;
    }
    public bool isAlive() {
        return alive;
    }
    public void KillConfirmed() {
        killsCount++;
    }
}
