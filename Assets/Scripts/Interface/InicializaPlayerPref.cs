using UnityEngine;
using System.Collections;

public class InicializaPlayerPref : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioListener.volume = PlayerPrefs.GetFloat("master_volume");
    }
}
