using UnityEngine;
using System.Collections;

public class InvunerabilityControl : MonoBehaviour {

    private GameObject shield;
    private Color barBackgroundColor;
    private Color barLoadingColor;
    private Color barLoadedColor;
    private GUIStyle backgroundStyle;
    private GUIStyle frontStyle;
    private float BAR_HEIGHT;
    private float InvunerabilityCounter;

    public static readonly float Invunerability_Time = 2.0f;
    public static readonly float Invunerability_Charge_Time = 10.0f;

    // Use this for initialization
    void Start () {
        InvunerabilityCounter = 0.0f;
        BAR_HEIGHT = Screen.height / 20;

        barBackgroundColor = Color.white;
        barLoadingColor = Color.green;
        barLoadedColor = Color.yellow;

        backgroundStyle = SetStyleColor(barBackgroundColor);
        frontStyle = SetStyleColor(barLoadingColor);
        shield = GameObject.Find("InvulShield");
        shield.SetActive(false);
    }

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
            InvunerabilityCounter = Mathf.Clamp(InvunerabilityCounter + Time.deltaTime, 0, Invunerability_Charge_Time);
           /* if (shield.activeSelf)
            {
                shield.transform.position = transform.position;
            }*/
        }

        if (InvunerabilityCounter >= Invunerability_Charge_Time) {
            if (InputControl.GetInvunerabilityUsed())
                StartCoroutine(InvunerabilityCoroutine());

            frontStyle = SetStyleColor(barLoadedColor);
        } else {
            frontStyle = SetStyleColor(barLoadingColor);
        }
    }

    IEnumerator InvunerabilityCoroutine() {
        if (InvunerabilityCounter >= Invunerability_Charge_Time) {
            //If invunerability is charged, disables collider, plays animation, and resets invunerability counter
            //isInvunerable = true;
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
            shield.transform.position = transform.position;
            shield.SetActive(true);
            
            InvunerabilityCounter = 0.0f;
            yield return new WaitForSeconds(Invunerability_Time);
            //After a few seconds, reset the colliders
            collider.enabled = true;
            shield.SetActive(false);
            //isInvunerable = false;
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
