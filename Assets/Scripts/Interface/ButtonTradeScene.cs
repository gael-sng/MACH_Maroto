using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonTradeScene : MonoBehaviour {

    public Button btnMenu;
    public enum opcao
    {
        Menu = 1,
        Game = 2,
        Creditos = 3,
        Opcoes = 4
    }
    public opcao cena;

    // Use this for initialization
    void Start () {
        if ((int)cena == 1)
        {
            btnMenu.onClick.AddListener(LoadSceneMenu);
        }
        else if ((int)cena == 2)
        {
            btnMenu.onClick.AddListener(LoadSceneJogo);
        }
        else if ((int)cena == 3)
        {
            btnMenu.onClick.AddListener(LoadSceneCreditos);
        }
        else if ((int)cena == 4)
        {
            btnMenu.onClick.AddListener(LoadSceneOpcoes);
        }

    }
	
	// Update is called once per frame
	void Update () {

    }

    void LoadSceneJogo()
    {
        SceneManager.LoadScene("Jogo");
    }

    void LoadSceneOpcoes()
    {
        SceneManager.LoadScene("Opcoes");
    }

    void LoadSceneCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    void LoadSceneMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
