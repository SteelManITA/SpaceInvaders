using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform enemy;
    private float minBound, maxBound;

    private Enemy model;

    public float fireRate = 0.997f;
    public float speed = 0.5f;
    public GameObject shot;

    void Move()
    {
        this.enemy.position += Vector3.right * this.speed;

        if (
            this.enemy.position.x < this.minBound
            || this.enemy.position.x > this.maxBound
        ) {
            this.speed = -this.speed;
        }

        foreach (Transform enemy in this.enemy) {
            if (enemy.position.x < this.minBound || enemy.position.x > this.maxBound) {
                this.speed = -this.speed;
                return;
            }

        }
    }

    void Start()
    {
        this.enemy = GetComponent<Transform>();
        this.minBound = this.maxBound = this.enemy.position.x;
        this.model = new Enemy();
        InvokeRepeating("Move", 3.0f, 1.0f);
    }

    void Update()
    {
        if (Random.value > this.fireRate) {
            Instantiate(
                this.shot,
                this.enemy.position,
                this.enemy.rotation
            );
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "BulletPlayer") {
            this.model.hurt(100);
            if (!this.model.isAlive()) {
                GameState.getInstance().incrementScore(100);
                Destroy(this.gameObject);
            }
        }
    }
}
