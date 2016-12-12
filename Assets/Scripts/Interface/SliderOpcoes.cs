using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Options sliders script
public class SliderOpcoes : MonoBehaviour {

    public Slider volumeSlider;
    public Slider calibratorSlider;

    // Use this for initialization
    public void Start()
    {
        volumeSlider.value = getMasterVolume(); //maximum volume
        calibratorSlider.value = getCalibrator(); //sensibility coefficient
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
	
	// Update is called once per frame
	void Update () {
        PlayerPrefs.SetFloat("master_volume", volumeSlider.value);
        PlayerPrefs.SetFloat("calibrator", calibratorSlider.value);
        AudioListener.volume = getMasterVolume();
    }

    //Volume option
    float getMasterVolume() 
    {
        return PlayerPrefs.GetFloat("master_volume", volumeSlider.maxValue);
    }

    //Sensibility option
    float getCalibrator()
    {
        return PlayerPrefs.GetFloat("calibrator", 3.0f);
    }

}
