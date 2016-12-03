using UnityEngine;
using System.Collections;

public class destroyZone : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Enemy");
			GameObject.Destroy (col.gameObject);
	}
}
