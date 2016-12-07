using UnityEngine;
using System.Collections;

public class InicializaPlayerPref : MonoBehaviour {

	//Changes volume according to player preference (options slider)
	void Start () {
        AudioListener.volume = PlayerPrefs.GetFloat("master_volume");
    }
}
