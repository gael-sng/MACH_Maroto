using UnityEngine;
using System.Collections;

public class UpgradeScript : MonoBehaviour {

    public float speed;
    private Vector2 dir;
    private Color[] randomColorArray = { Color.white, Color.blue, Color.green, Color.red };
    private Color colorA, colorB;
    private int index;
    private float counter;

    void Start() {
        dir = new Vector2(Random.value - 0.5f, -2.0f).normalized * speed * Time.deltaTime;
        index = Random.Range(0, 3);
        colorA = randomColorArray[index];
        colorB = randomColorArray[(++index) % 4];
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(dir);

        GetComponent<SpriteRenderer>().color = Color.Lerp(colorA, colorB, counter);
        if (GetComponent<SpriteRenderer>().color == colorB) {
            colorA = colorB;
            colorB = randomColorArray[(++index) % 4];
            counter = 0.0f;
        }
        counter += Time.deltaTime;

    }
}