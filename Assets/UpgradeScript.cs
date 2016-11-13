using UnityEngine;
using System.Collections;

public class UpgradeScript : MonoBehaviour {

    public float speed;

	// Update is called once per frame
	void Update () {
        Vector2 dir = new Vector2(Random.value, -1.0f) * speed * Time.deltaTime;
        transform.Translate(dir);
	}
}
