using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {

    public float speed;
    public float maxVerticalPosition;
    private Camera mainCamera;
    // Use this for initialization
    void Awake () {
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 dir;//dir.x between 0 and 1. Same for dir.y
        dir = getInput();

        if (!dir.Equals(Vector2.zero))
            movePlayer(dir);
    }

    private Vector2 getInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void movePlayer(Vector2 dir)
    {
        Vector3 newPosition = transform.position + new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(newPosition.x, GetMinHorizontalPosition(), GetMaxHorizontalPosition()),
                                         Mathf.Clamp(newPosition.y, GetMinVerticalPosition(), GetMaxVerticalPosition()), 0);
    }

    public float GetMinHorizontalPosition()
    {
        return - mainCamera.orthographicSize * Screen.width / Screen.height;
    }
    public float GetMaxHorizontalPosition()
    {
        return mainCamera.orthographicSize * Screen.width / Screen.height;
    }
    public float GetMinVerticalPosition()
    {
        return - mainCamera.orthographicSize;
    }
    public float GetMaxVerticalPosition()
    {
        return  mainCamera.orthographicSize;
    }


}
