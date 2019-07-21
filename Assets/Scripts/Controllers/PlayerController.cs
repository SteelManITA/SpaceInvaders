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

    public float speed = 100f;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;


    void Start()
    {
        this.player = GetComponent<Transform>();
        this.minBound = Constants.GetMinBoundX();
        this.maxBound = Constants.GetMaxBoundX();
        this.state = GameState.getInstance();

        this.state.setPlayer(new Player());
    }

    void Update()
    {
        if (Time.time > this.nextFire) {
            this.nextFire = Time.time + this.fireRate;
            Instantiate(this.shot, this.shotSpawn.position, this.shotSpawn.rotation);
        }

        if (Input.GetMouseButton(0)) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            (GetComponent<Rigidbody2D>()).velocity = new Vector2(direction.x * speed, direction.y * speed);
        } else {
            (GetComponent<Rigidbody2D>()).velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BulletEnemy") {
            Player player = this.state.getPlayer();

            player.hurt(100);
            if (!player.isAlive()) {
                Destroy(this.gameObject);
            }
        }
    }

}
