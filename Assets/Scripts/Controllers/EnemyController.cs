using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform enemy;
    private float minBound, maxBound;
    private int direction = 1;
    private Enemy model;
    private GameState state;

    public GameObject shot;

    void Move()
    {
        this.enemy.position += Vector3.right * (this.direction * this.model.getMovementSpeed());

        if (
            this.enemy.position.x < this.minBound
            || this.enemy.position.x > this.maxBound
        ) {
            this.direction = -this.direction;
        }

        foreach (Transform enemy in this.enemy) {
            if (enemy.position.x < this.minBound || enemy.position.x > this.maxBound) {
                this.direction = -this.direction;
                return;
            }

        }
    }

    void Start()
    {
        this.state = GameState.getInstance();
        this.enemy = GetComponent<Transform>();
        this.minBound = this.maxBound = this.enemy.position.x;
        int level = this.state.getLevel();
        this.model = new Enemy(level);
        InvokeRepeating("Move", 3.0f, 1.0f);

        float val = this.model.getFireDelay();
        InvokeRepeating("Shot", 1.0f + Random.value * val, val);
        this.state.setEnemyDamage(this.model.getAttack());
    }

    void Shot()
    {
        Instantiate(
            this.shot,
            this.enemy.position,
            this.enemy.rotation
        );
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "BulletPlayer") {
            this.model.hurt(this.state.getPlayer().getAttack());
            if (!this.model.isAlive()) {
                this.state.incrementScore(100);
                Destroy(this.gameObject);
            }
        }
    }
}
