using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonControl : MonoBehaviour {
    

    public void LoadOptions() {
        print("OPTIONS");
        SceneManager.LoadScene("Opcoes");
    }
    public void LoadCredits() {
        SceneManager.LoadScene("Creditos");
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void LoadGame() {
#if UNITY_EDITOR || UNITY_STANDALONE
        SceneManager.LoadScene("Jogo");
#elif UNITY_ANDROID
        SceneManager.LoadScene("PreJogo");
#endif
    }
}
