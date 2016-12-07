using UnityEngine;
using System.Collections;

//Displays current play time on screen
public class displayTime : Utilities {

    public GameObject Player;
    
    
    void Start()
    {
        //Initializes position of timer relative to screen size
        float aux = (GetMaxHorizontalPosition() - GetMinHorizontalPosition()) / 2.0f;
        transform.localScale = new Vector3(aux * 0.07f, aux * 0.07f, 1);
        transform.position = new Vector3(1.6f*aux / 3.0f, GetMaxVerticalPosition()-aux/25.0f, -1);


    }

    // Updates time display
    void Update()
    {
		if(Player != null)this.GetComponent<TextMesh>().text = "Time: " + Player.GetComponent<PlayerControl>().getTimeAlive().ToString("F0")+"s";
    }
}
