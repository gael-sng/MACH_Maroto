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
	public void ClearConsole () {
		// This simply does "LogEntries.Clear()" the long way:
		var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
		var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke(null,null);
	}

	public float RadianToDegree(float angle){
		return angle * (180.0f / Mathf.PI);
	}

}
