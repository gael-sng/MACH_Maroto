using UnityEngine;
using System.Collections;

//Makes ship move on main menu
public class MenuShipMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().Rotate(0, 0, (Mathf.PingPong(Time.time, 2.0f) - 1.0f)*0.3f);
	}
}
