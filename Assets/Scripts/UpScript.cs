using UnityEngine;
using System.Collections;

public class UpScript : MonoBehaviour {
    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "PlayerControler") {
            col.gameObject.SendMessage("UpgradeBullet");
            Destroy(col.gameObject);
        }
    }
}
