using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Behaviour for credits button - going back to the main menu
public class CreditsButtonScript : MonoBehaviour {

	public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
}
