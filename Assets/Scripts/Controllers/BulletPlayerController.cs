using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerController : MonoBehaviour
{
    private Transform bullet;
    public float speed;

    void Start()
    {
        this.bullet = GetComponent<Transform>();
    }

    // per essere sicuri che si muova sempre alla stessa velocità e non a quella del framerate
    void FixedUpdate()
    {
        this.bullet.position += Vector3.up * this.speed;

        if (this.bullet.position.y >= Constants.GetMaxBoundY()) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            Destroy(this.gameObject);
        }
    }
}
