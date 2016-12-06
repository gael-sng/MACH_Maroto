using UnityEngine;
using System.Collections;

public class InicializaPlayerPref : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetFloat("master_volume", 0.5f);
        PlayerPrefs.SetFloat("calibrator", 5.0f);   
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
