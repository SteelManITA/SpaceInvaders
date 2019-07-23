using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform enemy;
    private float minBound, maxBound;
    private Enemy model;
    private GameState state;

    public GameObject shot;
    public GameObject powerUp;

    void Start()
    {
        this.state = GameState.getInstance();
        this.enemy = GetComponent<Transform>();
        this.minBound = this.maxBound = this.enemy.position.x;
        this.model = new Enemy(this.state.getLevel());
        this.state.setEnemyDamage(this.model.getAttack());
        StartCoroutine("Move");
        StartCoroutine("Shot");
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(1.0f + Random.value * this.model.getFireDelay());
        while (true) {
            GameObject shotInstance = Instantiate(
                this.shot,
                this.enemy.position,
                this.enemy.rotation
            );
            shotInstance.GetComponent<Renderer>().material.color = Color.red;
            BulletController controller = shotInstance.GetComponent<BulletController>();
            controller.setDamage(this.model.getAttack());
            controller.setSpeed(-0.1f);
            controller.tag = "BulletEnemy";

            yield return new WaitForSeconds(this.model.getFireDelay());
        }
    }

    IEnumerator Move()
    {
        int direction = 1;

        yield return new WaitForSeconds(3.0f);

        while (true) {
            this.enemy.position += Vector3.right * (direction * this.model.getMovementSpeed());

            if (
                this.enemy.position.x < this.minBound
                || this.enemy.position.x > this.maxBound
            ) {
                direction = -direction;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "BulletPlayer") {
            this.model.hurt(this.state.getPlayer().getAttack());
            if (!this.model.isAlive()) {
                this.state.incrementScore(100);

                if (Random.value < this.model.getDropRate()) {
                    Instantiate(
                        this.powerUp,
                        this.enemy.position,
                        this.enemy.rotation
                    );
                }

                StopAllCoroutines();
                Destroy(this.gameObject);
            }
        }
    }
}
