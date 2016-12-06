using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Calibrator : MonoBehaviour {
#if UNITY_EDITOR
#elif UNITY_ANDROID
    // Update is called once per frame
    void Update () {

	    if (Input.touchCount > 0) {
            InputControl.calibrateAccelerometer();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
	}
#endif
}
