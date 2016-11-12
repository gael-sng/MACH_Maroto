using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    [Header("Max move speed")]
    public float speed;
    [Header("Max vertical position between 0(bot) and 1(top)")]
    public float maxVerticalPosition;

    private SpriteRenderer sprite;
    private Camera mainCamera;
    // Use this for initialization
    void Awake () {
        sprite = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 dir;//dir.x between 0 and 1. Same for dir.y
        dir = getInput();

        if (!dir.Equals(Vector2.zero))
            movePlayer(dir);
    }

    private Vector2 getInput() {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void movePlayer (Vector2 dir) {
        Vector3 newPosition = transform.position + new Vector3(dir.x, dir.y, 0) *speed * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(newPosition.x, GetMinHorizontalPosition(), GetMaxHorizontalPosition()),
                                         Mathf.Clamp(newPosition.y, GetMinVerticalPosition(), GetMaxVerticalPosition()), 0);
    }

    public float GetPlayerWidth() {
        return sprite.sprite.bounds.size.x;
    }
    public float GetPlayerHeight() {
        return sprite.sprite.bounds.size.y;
    }
    public float GetMinHorizontalPosition() {
        return GetPlayerWidth() / 2.0f - mainCamera.orthographicSize * Screen.width / Screen.height;
    }
    public float GetMaxHorizontalPosition() {
        return mainCamera.orthographicSize*Screen.width/Screen.height - GetPlayerWidth() / 2.0f;
    }
    public float GetMinVerticalPosition() {
        return GetPlayerHeight()/2.0f - mainCamera.orthographicSize;
    }
    public float GetMaxVerticalPosition() {
        return (2*maxVerticalPosition-1) * mainCamera.orthographicSize;
    }



    void OnCollisionEnter(Collision col) {
        print("Coll");
    }
    void OnTriggerEnter(Collider col) {
        print("TRIGGER");
    }
}
