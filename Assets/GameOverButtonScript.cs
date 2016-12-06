using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverButtonScript : MonoBehaviour {

	// Use this for initialization
	public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void LoadGame() {
#if UNITY_EDITOR || UNITY_STANDALONE
        SceneManager.LoadScene("Jogo");
#elif UNITY_ANDROID
        SceneManager.LoadScene("PreJogo");
#endif
    }
}
