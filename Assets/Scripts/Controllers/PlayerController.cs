using System;
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
                    this.InstantiateShot(position, rotation, this.model.getAttack());
                    break;
                }
                case Player.ShotType.radius: {
                    // TODO: fix diagonal shot
                    int powerUp = this.model.getPowerUpCount();
                    int shots = Mathf.Min(3 + powerUp * 2, 11);
                    int damagePerShot = this.model.getAttack() / shots;
                    Debug.Log("radius " + powerUp + " " + shots + " " + damagePerShot);
                    for (int i = 0; i < shots; ++i) {
                        rotation = transform.rotation;

                        if (i == 0) {
                            this.InstantiateShot(position, rotation, damagePerShot);
                            continue;
                        }

                        if (i % 2 == 0) {
                            int offset = i/2;
                            rotation.z = 9f * offset;
                            this.InstantiateShot(position, rotation, damagePerShot);
                            // ruota a sinistra
                        } else if (i % 2 != 0) {
                            int offset = i/2;
                            rotation.z = 9f * -offset;
                            this.InstantiateShot(position, rotation, damagePerShot);
                            // ruota a destra
                        }
                    }
                    break;
                }
                case Player.ShotType.wall: {
                    int powerUp = this.model.getPowerUpCount();
                    int shots = Mathf.Min(3 + powerUp * 2, 9);
                    int damagePerShot = this.model.getAttack() / shots;
                    Debug.Log("radius " + powerUp + " " + shots + " " + damagePerShot);
                    for (int i = 0; i < shots; ++i) {
                        position = transform.position;

                        if (i == 0) {
                            this.InstantiateShot(position, rotation, damagePerShot);
                            continue;
                        }

                        if (i % 2 == 0) {
                            int offset = i/2;
                            position.x += offset * 0.2f;
                            this.InstantiateShot(position, rotation, damagePerShot);
                        } else if (i % 2 != 0) {
                            int offset = i/2 + 1;
                            position.x -= offset * 0.2f;
                            this.InstantiateShot(position, rotation, damagePerShot);
                        }
                    }
                    break;
                }
                default: {
                    throw new System.Exception("ShotTypeError: unrecognized ShotType");
                }
            }

            yield return new WaitForSeconds(this.model.getFireDelay());
        }
    }

    private void InstantiateShot(Vector3 position, Quaternion rotation, int damage) {
        GameObject shotInstance = Instantiate(
            this.shot,
            position,
            rotation
        );
        shotInstance.GetComponent<Renderer>().material.color = Color.green;
        BulletController controller = shotInstance.GetComponent<BulletController>();
        controller.setDamage(damage);
        controller.setSpeed(0.3f);
        controller.tag = "BulletPlayer";
    }

    private Vector3 lastAcceleration = Vector3.right;

    void Update()
    {
        bool accelerometer = Convert.ToBoolean(PlayerPrefs.GetInt("Accelerometer", 0));

        if (accelerometer) {
            Vector3 acceleration = Input.acceleration * 0.3f;
            (GetComponent<Rigidbody2D>()).velocity = new Vector2(acceleration.x * this.model.getMovementSpeed(), acceleration.y * this.model.getMovementSpeed());

        } else {
            if (Input.GetMouseButton(0)) {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - transform.position).normalized;
                (GetComponent<Rigidbody2D>()).velocity = new Vector2(direction.x * this.model.getMovementSpeed(), direction.y * this.model.getMovementSpeed());
            } else {
                (GetComponent<Rigidbody2D>()).velocity = Vector2.zero;
            }
        }

        if (
            this.player.position.x < this.minBound
            || this.player.position.x > this.maxBound
        ) {
            Debug.Log(Vector3.left + " " + this.maxBound + " " + Vector3.left * this.maxBound);
            this.player.position = new Vector3(
                this.player.position.x < this.minBound ? this.minBound : this.maxBound,
                this.player.position.y,
                this.player.position.z
            );
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
