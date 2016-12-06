using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderOpcoes : MonoBehaviour {

    public Slider volumeSlider;
    public Slider calibratorSlider;

    // Use this for initialization
    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
	
	// Update is called once per frame
	void Update () {
        PlayerPrefs.SetFloat("master_volume", volumeSlider.value);
        PlayerPrefs.SetFloat("calibrator", calibratorSlider.value);
    }

    float getMasterVolume()
    {
        return PlayerPrefs.GetFloat("master_volume");
    }

    float getCalibrator()
    {
        return PlayerPrefs.GetFloat("calibrator");
    }

}
