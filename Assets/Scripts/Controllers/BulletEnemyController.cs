using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    private Transform bullet;
    private float speed = 0.1f;

    void Start()
    {
        this.bullet = GetComponent<Transform>();
    }

    // per essere sicuri che si muova sempre alla stessa velocità e non a quella del framerate
    void FixedUpdate()
    {
        this.bullet.position += Vector3.up * -this.speed;

        if (this.bullet.position.y <= Constants.GetMinBoundY()) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Destroy(this.gameObject);
        }
    }
}
