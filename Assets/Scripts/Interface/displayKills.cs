using UnityEngine;
using System.Collections;

//Script for displaying the number of kills made by the player
public class displayKills : Utilities {

    public GameObject Player;


	void Start () {
        //Calculates position relative to size of screen
        float aux = (GetMaxHorizontalPosition() - GetMinHorizontalPosition())/2.0f;
        transform.localScale = new Vector3(aux * 0.07f, aux * 0.07f, 1);
        transform.position = new Vector3(0.5f*aux / 3.0f, GetMaxVerticalPosition() - aux / 25.0f, -1);
    }
	
	// Updates number of kills
	void Update () {
		if(Player != null)this.GetComponent<TextMesh>().text ="Kills: "+Player.GetComponent<PlayerControl>().getKillsCount();
	}
}
