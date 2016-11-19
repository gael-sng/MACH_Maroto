using UnityEngine;
using System.Collections;

public class loopBackground : MonoBehaviour {

    public float speed=0;
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0f, (Time.time*speed)%1);
	}
}
