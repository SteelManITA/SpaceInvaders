using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private Transform powerUp;
    private float speed = 0.05f;

    void Start()
    {
        this.powerUp = GetComponent<Transform>();
    }

    // per essere sicuri che si muova sempre alla stessa velocità e non a quella del framerate
    void FixedUpdate()
    {
        this.powerUp.position += Vector3.up * -this.speed;

        if (this.powerUp.position.y <= Constants.GetMinBoundY()) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Destroy(this.gameObject);
        }
    }
}
