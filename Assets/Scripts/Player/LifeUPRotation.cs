using UnityEngine;
using System.Collections;

//Fixes rotation on lifeUp icon
public class LifeUPRotation : MonoBehaviour {
    void Update() {
        transform.rotation = Quaternion.identity;
    }
}