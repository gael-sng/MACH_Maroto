using UnityEngine;
using System.Collections;

public class UpgradeScript : MonoBehaviour {

    public float speed;
    private Vector2 dir;

    void Start() {
        dir = new Vector2(Random.value-0.5f, -2.0f).normalized * speed * Time.deltaTime;
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(dir);
	}
}
