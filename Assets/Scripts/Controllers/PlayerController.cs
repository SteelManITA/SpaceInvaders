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

    void Start()
    {
        this.player = GetComponent<Transform>();
        this.minBound = Constants.GetMinBoundX();
        this.maxBound = Constants.GetMaxBoundX();
        this.model = new Player();
        this.state = GameState.getInstance();

        this.state.setPlayer(this.model);
        StartCoroutine("Shot");
    }

    IEnumerator Shot()
    {
        while (true) {
            Player.ShotType shotType = this.model.getShotType();
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;

            switch (shotType) {
                case Player.ShotType.linear: {
                    position = transform.position;
                    rotation = transform.rotation;
                    break;
                }
                case Player.ShotType.radius: {

                    break;
                }
                case Player.ShotType.wall: {

                    break;
                }
                default: {
                    throw new System.Exception("ShotTypeError: unrecognized ShotType");
                }
            }

            GameObject shotInstance = Instantiate(
                this.shot,
                position,
                rotation
            );
            shotInstance.GetComponent<Renderer>().material.color = Color.green;
            BulletController controller = shotInstance.GetComponent<BulletController>();
            controller.setDamage(this.model.getAttack());
            controller.setSpeed(0.3f);
            controller.tag = "BulletPlayer";

            yield return new WaitForSeconds(this.model.getFireDelay());
        }
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
            this.model.hurt(other.gameObject.GetComponent<BulletController>().getDamage());
            if (!this.model.isAlive()) {
                Destroy(this.gameObject);
            }
        } else if (other.tag == "PowerUp") {
            this.model.powerUp();
        }
    }

}
