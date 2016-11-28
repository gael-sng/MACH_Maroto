using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : ShipScript {
    
    [Header("Max vertical position between 0(bot) and 1(top)")]
    public float maxVerticalPosition;
    
    private Camera mainCamera;
    private float score;
    private bool alive;
    private float InvunerabilityCounter;

    public static readonly float Invunerability_Time = 1.0f;
    public static readonly float Invunerability_Charge_Time = 10.0f;

    public static readonly float SHIP_WIDTH = 0.7f;
    public static readonly float SHIP_HEIGHT = 0.7f;

    private Vector3 defaultAngle;

    void Start() {
        defaultAngle = transform.eulerAngles;
        mainCamera = Camera.main;
        score = 0;
        alive = true;
        InvunerabilityCounter = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 dir;//dir.x between 0 and 1. Same for dir.y
        dir = InputControl.GetMoveDirection();

        if (!dir.Equals(Vector2.zero))
            MoveShip(dir);

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
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
            InvunerabilityCounter = 0.0f;
            yield return new WaitForSeconds(Invunerability_Time);
            //After a few seconds, reset the colliders
            collider.enabled = true;
        }
    }
    
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "EnemyBullet") {
            TakeDamage(col.gameObject.GetComponent<bulletScript>().GetDamage());
            col.gameObject.SendMessage("Destroy");
        } else if (col.gameObject.tag == "Enemy") {
            TakeDamage(hitPoints);
        }else if (col.gameObject.tag == "UP") {
            GetComponent<Shooting>().UpgradeBullet();
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
        SceneManager.LoadScene(0);

    }

    public override void MoveShip(Vector3 dir) {
        Vector3 newPosition = transform.position + new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;

        newPosition = new Vector3(Mathf.Clamp(newPosition.x, GetMinHorizontalPosition(), GetMaxHorizontalPosition()),
                                         Mathf.Clamp(newPosition.y, GetMinVerticalPosition(), GetMaxVerticalPosition()), 0);


        //if (GetComponent<Collider>().enabled)
		//transform.rotation = Quaternion.Euler(new Vector3(-dir.y * xFlipCoef, -dir.x * yFlipCoef, 0.0f));

        transform.position = newPosition;

    }
    public float GetMinHorizontalPosition() {
        return SHIP_WIDTH / 2.0f - mainCamera.orthographicSize * Screen.width / Screen.height;
    }
    public float GetMaxHorizontalPosition() {
        return mainCamera.orthographicSize * Screen.width / Screen.height - SHIP_WIDTH / 2.0f;
    }
    public float GetMinVerticalPosition() {
        return SHIP_HEIGHT / 2.0f - mainCamera.orthographicSize;
    }
    public float GetMaxVerticalPosition() {
        return (2*maxVerticalPosition-1) * mainCamera.orthographicSize;
    }
}
