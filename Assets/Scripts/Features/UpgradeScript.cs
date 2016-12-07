using UnityEngine;
using System.Collections;

//Makes the upgrade item flash in different colors on screen and moves it downwards
public class UpgradeScript : MonoBehaviour {

    public float speed;
    private Vector2 dir;
    private Color[] randomColorArray = { Color.white, Color.blue, Color.green, Color.red };
    private Color colorA, colorB;
    private int index;
    private float counter;

    void Start() {
        dir = new Vector2(Random.value - 0.5f, -2.0f).normalized * speed * Time.deltaTime; //Movement direction
        index = Random.Range(0, 3); //Initializes colors
        colorA = randomColorArray[index];
        colorB = randomColorArray[(++index) % 4];
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(dir);

        GetComponent<SpriteRenderer>().color = Color.Lerp(colorA, colorB, counter); //Makes color change from A to B
        if (GetComponent<SpriteRenderer>().color == colorB) {
            colorA = colorB;
            colorB = randomColorArray[(++index) % 4];
            counter = 0.0f;
        }
        counter += Time.deltaTime;

    }
}