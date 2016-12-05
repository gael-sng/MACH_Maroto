using UnityEngine;
using System.Collections;

//JUST FOR TESTING PURPOSES

public class SimpleMovement : MonoBehaviour {

    public float speed;
    public float maxVerticalPosition;
    private Camera mainCamera;
    private Animator animator;
    public GameObject playerExplosion;
    // Use this for initialization
    void Awake () {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 dir;//dir.x between 0 and 1. Same for dir.y
       

        dir = getInput();

        if (dir.y < 0)
            //Explode();
       
        if (dir.x > 0)
            animator.SetInteger("lean", 1);
        else
            if (dir.x < 0)
            animator.SetInteger("lean", -1);
        else
            animator.SetInteger("lean", 0);

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


    void Explode()
    {
        Instantiate(playerExplosion, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "EnemyBullet") {
			col.gameObject.SendMessage ("Destroy");
			Explode ();
		}
	}
}
