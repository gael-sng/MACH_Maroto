using UnityEngine;
using System.Collections;

//Player's special move: a rechargeable invulnerability shield
public class InvunerabilityControl : MonoBehaviour {

    private GameObject shield; //Model for the shield - should be attached to player model
    //Loading bar for invulnerability
    private Color barBackgroundColor;
    private Color barLoadingColor;
    private Color barLoadedColor;
    private GUIStyle backgroundStyle;
    private GUIStyle frontStyle;
    private float BAR_HEIGHT;
    private float InvunerabilityCounter; //Time counter for recharging the shield

    public static readonly float Invunerability_Time = 2.0f; //Invulnerability time
    public static readonly float Invunerability_Charge_Time = 10.0f; //Recharge time

    
    void Start () {
        InvunerabilityCounter = 0.0f;

        //Initializes loading bar
        BAR_HEIGHT = Screen.height / 20;
        barBackgroundColor = Color.white;
        barLoadingColor = Color.green;
        barLoadedColor = Color.yellow;

        backgroundStyle = SetStyleColor(barBackgroundColor);
        frontStyle = SetStyleColor(barLoadingColor);

        //Initializes shield
        shield = GameObject.Find("InvulShield");
        shield.SetActive(false);
    }

    //Loading bar style
    private GUIStyle SetStyleColor(Color newColor) {
        Texture2D newTexture = new Texture2D(1, 1);
        newTexture.SetPixel(1, 1, newColor);
        newTexture.Apply();
        GUIStyle style = new GUIStyle();
        style.normal.background = newTexture;

        return style;
    }

    // Update is called once per frame
    void Update () {
	    if (GetComponent<PlayerControl>().isAlive()) {
            //Increases timer
            InvunerabilityCounter = Mathf.Clamp(InvunerabilityCounter + Time.deltaTime, 0, Invunerability_Charge_Time);
        }

        if (InvunerabilityCounter >= Invunerability_Charge_Time) { //Shield charged
            if (InputControl.GetInvunerabilityUsed()) //Shield initiated by player
                StartCoroutine(InvunerabilityCoroutine());

            frontStyle = SetStyleColor(barLoadedColor);
        } else {
            frontStyle = SetStyleColor(barLoadingColor);
        }
    }

    IEnumerator InvunerabilityCoroutine() {
        if (InvunerabilityCounter >= Invunerability_Charge_Time) {
            //If invunerability is charged, disables collider, plays animation, and resets invunerability counter
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
         
            shield.SetActive(true);
            
            InvunerabilityCounter = 0.0f;
            yield return new WaitForSeconds(Invunerability_Time);
            //After a few seconds, reset the colliders
            collider.enabled = true;
            shield.SetActive(false);
        }
    }

    void OnGUI() {
        GUI.Box(new Rect(0, Screen.height - BAR_HEIGHT, Screen.width, BAR_HEIGHT),"", backgroundStyle);
        GUI.Box(new Rect(0, Screen.height - BAR_HEIGHT, Screen.width * (InvunerabilityCounter / Invunerability_Charge_Time), BAR_HEIGHT), "", frontStyle);
    }

    public float GetBarHeight() {
        return BAR_HEIGHT;
    }
}
