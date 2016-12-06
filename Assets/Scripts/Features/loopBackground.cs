using UnityEngine;
using System.Collections;

//This class shifts the texture of the background in order to make it seem like its moving
public class loopBackground : MonoBehaviour {

    public float speed=0;
	
	void Update () {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0f, (Time.time*speed)%1);
	}
}
