using UnityEngine;
using System.Collections;

public class InicializaSomCalibrator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetFloat("master_volume", 1.0f);
        PlayerPrefs.SetFloat("calibrator", 5.0f);   
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
