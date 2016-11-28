using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

    [Header("Hitpoints")]
    public float hitPoints;

    [Header("Max move speed")]
    public float speed;

    public static readonly float yFlipCoef = 30.0f;
    public static readonly float xFlipCoef = 10.0f;


    protected void TakeDamage(float damage) {

        hitPoints -= damage;

        if (hitPoints <= 0.0f) {
            DestroyShip();
        }
    }

    public virtual void MoveShip(Vector3 dir) { }

    // Use this for initialization
    public virtual void DestroyShip() {
        //Instantiate(explosion);
        Destroy(gameObject);
    }
}
