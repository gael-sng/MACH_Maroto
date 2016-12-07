using UnityEngine;
using System.Collections;

//Used for destroying enemies who go past the screen
public class destroyZone : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Enemy")
			GameObject.Destroy (col.gameObject);
	}
}
