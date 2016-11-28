using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    [Header("Enemy Prefab")]
    public GameObject enemyPrefab;

    [Header("Seconds between spawn")]
    public float timer;

    float counter = 0.0f;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 randomPosition = Camera.main.ViewportToWorldPoint(new Vector2(Camera.main.rect.xMax, Camera.main.rect.yMax) - Vector2.right* (Random.Range(0.0f, Camera.main.rect.width))) + Vector3.up;

        if (counter >= timer) {
            Instantiate(enemyPrefab, randomPosition, Quaternion.Euler(0,90,-90));
            counter = 0;
        } else {
            counter += Time.deltaTime;
        }
	}
}
