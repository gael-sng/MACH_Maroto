using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

    [Header("Hitpoints")]
    public float hitPoints;

    [Header("Max move speed")]
    public float speed;

    protected void TakeDamage(float damage) {

        hitPoints -= damage;

        if (hitPoints <= 0.0f) {
            DestroyShip();
        }
    }
    // Use this for initialization
    public virtual void DestroyShip() {
        //Instantiate(explosion);
        Destroy(gameObject);
    }
}
