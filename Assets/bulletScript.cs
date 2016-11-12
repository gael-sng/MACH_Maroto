using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start () {
        Destroy(transform.gameObject, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
}
