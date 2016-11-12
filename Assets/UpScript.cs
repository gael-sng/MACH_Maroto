using UnityEngine;
using System.Collections;

public class UpScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider col) {
        print("SHIT");
        if (col.gameObject.tag == "PlayerControler") {
            col.gameObject.SendMessage("UpgradeBullet");
            Destroy(col.gameObject);
        }
    }
}
