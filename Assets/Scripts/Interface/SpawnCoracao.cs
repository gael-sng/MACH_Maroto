using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpawnCoracao : Utilities {
	private int currentLives = 0;
	public static readonly int maxLifes = 10;
    public GameObject live;
    private GameObject[] countLives = new GameObject[10];
	private Vector3 positionVidas;
	private float aux;
	public int initialHearts;

    // Use this for initialization
    void Start () {
		if (initialHearts <= 0)
			initialHearts = 3;
		aux = (GetMaxHorizontalPosition() - GetMinHorizontalPosition())/12.0f;
        live.GetComponent<SpriteRenderer> ().bounds.size.Set(aux, aux, 0);
        live.transform.localScale = new Vector3(10, 10, 1);
        
        positionVidas = new Vector3 (aux * 1.5f + GetMinHorizontalPosition(), -1.0f * aux + GetMaxVerticalPosition(), 0);
		print ("aux = " + aux);
		for (int i=0; i < maxLifes; i++)
        {
            print("Spawn coracao " + i);
            countLives[i] = (GameObject)Instantiate(live, positionVidas, Quaternion.identity);
            countLives[i].SetActive (false);
            positionVidas = positionVidas + Vector3.right * aux;
        }
		for (int i = 0; i < initialHearts; i++)
			AddHeart ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RemoveHeart();
        }else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddHeart();
		}else if (currentLives == 0)
        {
            SceneManager.LoadScene("Game Over");//carrega cena gameover
        }
    }
    public void AddHeart()
    {
		if (currentLives < 10) {
            countLives[currentLives].SetActive(true);
			currentLives++;
        }       
    }
    public void RemoveHeart()
    {
		currentLives--;
        countLives[currentLives].SetActive(false);
    }
}
