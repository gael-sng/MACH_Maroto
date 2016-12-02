using UnityEngine;
using System.Collections;

public class CameraBehaviour : Utilities {
	public GameObject destroyZoneObject;
	// Use this for initialization
	void Start () {
		if (destroyZoneObject) {
			destroyZoneObject.transform.localScale = new Vector3 (GetMaxHorizontalPosition () - GetMinHorizontalPosition (), 0.1f, 1f);
			Instantiate (destroyZoneObject, new Vector3 (0, GetMinVerticalPosition () - 2.0f, 0), Quaternion.identity);
		}
	}
}
