using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PreJogoScript : MonoBehaviour {
#if UNITY_EDITOR
#elif UNITY_ANDROID
    void Update(){
        if (Input.touchCount > 0) LoadGame();
    }
#endif

    void Start() {
        GameObject.Find("coeficientText").GetComponent<TextMesh>().text = 
            "Your Battle Coeficient is " + ScoreSystem.GetUserRankCoeficient().ToString("F2");
    }

    void LoadGame() {

        //Calibrate
#if UNITY_EDITOR
#elif UNITY_ANDROID
        InputControl.calibrateAccelerometer();
#endif

        SceneManager.LoadScene("Jogo");
    }
}