using UnityEngine;
using System.Collections;

public class Utilities : MonoBehaviour {

	public float GetMinHorizontalPosition()
	{
		return - Camera.main.orthographicSize * Screen.width / Screen.height;
	}
	public float GetMaxHorizontalPosition()
	{
		return Camera.main.orthographicSize * Screen.width / Screen.height;
	}
	public float GetMinVerticalPosition()
	{
		return - Camera.main.orthographicSize;
	}
	public float GetMaxVerticalPosition()
	{
		return  Camera.main.orthographicSize;
	}
}
