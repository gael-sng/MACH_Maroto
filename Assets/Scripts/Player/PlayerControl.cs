using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : ShipScript {
    
    [Header("Max vertical position between 0(bot) and 1(top)")]
    public float maxVerticalPosition;
    
    private Camera mainCamera;
    private float score;
	private bool alive;//, isInvunerable;
    private float InvunerabilityCounter;
    public GameObject life;
	public GameObject lifeaBackground;
    private Vector3 positionVidas;
    private float aux;
    private GameObject[] countLives = new GameObject[10];
    private Quaternion barrelDefault;
    private float countdown = 10.0f;        //Variavel para contagem de tempo para ganhar nova vida (decidir um tempo bom para ganhar novas vidas)
    public GameObject lifeUP;               //GameObject para imagem de LifeUP
    private bool vidaSwitch = false;
    private GameObject lifeUPRunTime;
    private Vector3 defaultAngle;
    

    public static readonly int maxLifes = 10;
    public static readonly float Invunerability_Time = 1.0f;
    public static readonly float Invunerability_Charge_Time = 10.0f;

    public static readonly float SHIP_WIDTH = 0.7f;
    public static readonly float SHIP_HEIGHT = 0.7f;

   

    void Start() {
        defaultAngle = transform.eulerAngles;
        barrelDefault = transform.GetChild(0).rotation;
        mainCamera = Camera.main;
        score = 0;
        alive = true;
        InvunerabilityCounter = 0.0f;

        aux = (GetMaxHorizontalPosition() - GetMinHorizontalPosition()) / 12.0f;
		lifeaBackground.transform.localScale = new Vector3(GetMaxHorizontalPosition() - GetMinHorizontalPosition(), aux * 0.5f, 1);
        positionVidas = new Vector3(aux * 1.5f + GetMinHorizontalPosition(), -0.5f * aux + GetMaxVerticalPosition(), -1);
		print ("x inicial = " + ( GetMinHorizontalPosition()));
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
   
        if (alive) {
            score += Time.deltaTime;
            InvunerabilityCounter = Mathf.Clamp(InvunerabilityCounter + Time.deltaTime, 0, Invunerability_Charge_Time);
        }

        if (InputControl.GetInvunerabilityUsed())
            StartCoroutine(InvunerabilityCoroutine());

        //For testing:
        //GameObject.Find("Condition").GetComponent<TextMesh>().text = "Scr:" + score.ToString("F1") + "/Highscr:" + PersistentData.GetHighscore().ToString("F1") + "/Inv:" + (InvunerabilityCounter>=Invunerability_Charge_Time);
    }

    IEnumerator InvunerabilityCoroutine() {
        if (InvunerabilityCounter >= Invunerability_Charge_Time) {
            //If invunerability is charged, disables collider, plays animation, and resets invunerability counter
            //isInvunerable = true;
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
            InvunerabilityCounter = 0.0f;
            yield return new WaitForSeconds(Invunerability_Time);
            //After a few seconds, reset the colliders
            collider.enabled = true;
            //isInvunerable = false;
        }
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
        
        if (score > PersistentData.GetHighscore()) {
            //Beats highscore
            PersistentData.SetHighscore(score);
        }
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
			//macaquisse que o cristiano não gostou, mudar depois
			transform.GetChild(0).rotation = barrelDefault;
			transform.GetChild(1).rotation = barrelDefault;
			transform.GetChild(2).rotation = barrelDefault;
			transform.GetChild(3).rotation = barrelDefault;
            transform.GetChild(4).rotation = barrelDefault;
        }
            

        transform.position = newPosition;

    }

    public void AddLive()
    {
        if (gameObject.GetComponent<PlayerControl>().hitPoints < 10)
        {
            
            lifeUPRunTime = (GameObject)Instantiate(lifeUP, new Vector3(this.transform.position.x, this.transform.position.y+SHIP_WIDTH, this.transform.position.z-1), Quaternion.identity); //Imagem de LIFEUP
            lifeUPRunTime.transform.localScale = new Vector3(0.1f, 0.1f, 1);             //Tamanho da imagem de aumento de vida
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
        return SHIP_HEIGHT / 2.0f - mainCamera.orthographicSize;
    }
	public float GetPlayerMaxVerticalPosition() {
        return (2*maxVerticalPosition-1) * mainCamera.orthographicSize;
    }
}
