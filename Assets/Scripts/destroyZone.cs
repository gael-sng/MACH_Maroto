using UnityEngine;
using System.Collections;

public class destroyZone : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyBullet")
			GameObject.Destroy (col.gameObject);

	}
}
