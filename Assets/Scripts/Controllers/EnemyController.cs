using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Transform enemy;
    protected float minBound, maxBound;
    protected IEnemy model;
    private Vector3 startPosition;
    protected GameState state;

    public GameObject shot;
    public GameObject powerUp;

    protected virtual void Awake()
    {
        this.state = GameState.getInstance();
        this.enemy = GetComponent<Transform>();
        this.minBound = this.maxBound = this.enemy.position.x;
        this.model = new Enemy(this.state.getLevel());
        this.startPosition = this.enemy.position;
    }

    protected void Start()
    {
        this.StartGameCoroutine(ComeInScene());
        this.StartGameCoroutine(Shot());
    }

    protected virtual void Update()
    {
        GetComponent<Rigidbody2D>().UpdateWithGameStatus();
    }

    protected IEnumerator WaitForShot(float time)
    {
        while (time > 0) {
            while (GameState.getInstance().getState() != GameState.State.Started) {
                yield return new WaitForSeconds(0.2f);
            }
            time -= 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }

    protected virtual IEnumerator Shot()
    {
        yield return WaitForShot(1.0f + Random.value * this.model.getFireDelay());

        while (true) {
            GameObject shotInstance = Instantiate(
                this.shot,
                this.enemy.position,
                this.enemy.rotation
            );
            shotInstance.GetComponent<Renderer>().material.color = Color.red;
            BulletController controller = shotInstance.GetComponent<BulletController>();
            controller.setDamage(this.model.getAttack());
            controller.setSpeed(0.1f);
            controller.tag = "BulletEnemy";

            yield return WaitForShot(this.model.getFireDelay());
        }
    }

    IEnumerator ComeInScene()
    {
        while (this.enemy.position.y > this.startPosition.y - 11) {
            this.enemy.position += new Vector3(0, -0.2f, 0);
            yield return new WaitForSeconds(.001f);
        }
        yield return Move();
    }

    protected virtual IEnumerator Move()
    {
        int direction = 1;

        yield return new WaitForSeconds(3.0f);

        (GetComponent<Rigidbody2D>()).velocity = new Vector2(-this.model.getMovementSpeed(), 0);
        yield return new WaitForSeconds(1.0f);

        while (true) {
            (GetComponent<Rigidbody2D>()).velocity = new Vector2(direction * this.model.getMovementSpeed(), 0);

            if (
                this.enemy.position.x < this.minBound
                || this.enemy.position.x > this.maxBound
            ) {
                direction = -direction;
            }

            yield return new WaitForSeconds(2.0f);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "BulletPlayer") {
            this.model.hurt(this.state.getPlayer().getAttack());
            if (!this.model.isAlive()) {
                this.state.incrementScore(this.model.getScore());

                if (Random.value < this.model.getDropRate()) {
                    Instantiate(
                        this.powerUp,
                        this.enemy.position,
                        this.enemy.rotation
                    );
                }

                StopAllCoroutines();
                SoundManager.getInstance().Explosion();
                Destroy(this.gameObject);
            }
        }
    }
}
