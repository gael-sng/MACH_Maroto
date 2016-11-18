using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpawnCoracao : MonoBehaviour {
    private int Lifes = 10;
    public GameObject Heart;
    private GameObject[] lifes = new GameObject[10];
    private Vector3 position = new Vector3(-10, 4, 0);
    private Vector3 aux = new Vector3(1, 0, 0);

    // Use this for initialization
    void Start () {
        
        for (int i=0; i< Lifes; i++)
        {
            print("Spawn coracao " + i);
            lifes[i] = (GameObject)Instantiate(Heart, position, Quaternion.identity);
            position = position + aux;
        }
        for (int i=3; i< Lifes; i++) {
            lifes[i].SetActive(false);
        }
        Lifes = 3;
        Debug.Log("start\n");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RemoveHeart();
        }else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddHeart();
        }else if (Lifes == 0)
        {
            SceneManager.LoadScene("Game Over");//carrega cena gameover
        }
    }
    void AddHeart()
    {
        if (Lifes < 10) {
            lifes[Lifes].SetActive(true);
            Lifes++;
        }            
    }
    void RemoveHeart()
    {
        Lifes--;
        lifes[Lifes].SetActive(false);
    }
}
