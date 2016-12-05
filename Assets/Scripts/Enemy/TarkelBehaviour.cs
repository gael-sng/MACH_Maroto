using UnityEngine;
using System.Collections;

public class TarkelBehaviour : Utilities {
	public Transform player;
	// Use this for initialization
	void Start () {
		gameObject.GetComponentInChildren<gatlingBarrel> ().player = player;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
