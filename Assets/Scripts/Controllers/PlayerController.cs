using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform player;
    private float maxBound, minBound;
    private Vector3 mousePosition;
    private Vector2 direction;
    private GameState state;
    private float nextFire;
    private Player model;

    public GameObject shot;
    public Transform shotSpawn;

    void Start()
    {
        this.player = GetComponent<Transform>();
        this.minBound = Constants.GetMinBoundX();
        this.maxBound = Constants.GetMaxBoundX();
        this.model = new Player();
        this.state = GameState.getInstance();

        this.state.setPlayer(this.model);
        Invoke("Shot", 0f);
    }

    void Shot()
    {
        Instantiate(
            this.shot,
            this.shotSpawn.position,
            this.shotSpawn.rotation
        );
        Invoke("Shot", this.model.getFireDelay());
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            (GetComponent<Rigidbody2D>()).velocity = new Vector2(direction.x * this.model.getMovementSpeed(), direction.y * this.model.getMovementSpeed());
        } else {
            (GetComponent<Rigidbody2D>()).velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BulletEnemy") {
            this.model.hurt(this.state.getEnemyDamage());
            if (!this.model.isAlive()) {
                Destroy(this.gameObject);
            }
        } else if (other.tag == "PowerUp") {
            this.model.powerUp();
        }
    }

}
