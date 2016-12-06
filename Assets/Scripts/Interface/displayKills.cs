using UnityEngine;
using System.Collections;

public class displayKills : Utilities {

    public GameObject Player;
	// Use this for initialization
	void Start () {
        float aux = (GetMaxHorizontalPosition() - GetMinHorizontalPosition())/2.0f;
        transform.localScale = new Vector3(aux * 0.07f, aux * 0.07f, 1);
        transform.position = new Vector3(0.5f*aux / 3.0f, GetMaxVerticalPosition() - aux / 25.0f, -1);


    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<TextMesh>().text ="Kills: "+Player.GetComponent<PlayerControl>().getKillsCount();
	}
}
