using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform bullet;
    private float speed = 0.1f;
    private int damage = 0;

    void Start()
    {
        this.bullet = GetComponent<Transform>();
    }

    // per essere sicuri che si muova sempre alla stessa velocità e non a quella del framerate
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().UpdateWithGameStatus();

        float constVal = 50f * this.speed;

        float angle = Mathf.Deg2Rad * ((GetComponent<Rigidbody2D>()).rotation - 90);

        (GetComponent<Rigidbody2D>()).velocity = new Vector2(
            Mathf.Cos(angle) * constVal,
            Mathf.Sin(angle) * constVal
        );

        if (
            this.bullet.position.y <= Constants.GetMinBoundY()
            || this.bullet.position.y >= Constants.GetMaxBoundY()
            || this.bullet.position.x <= Constants.GetMinBoundX()
            || this.bullet.position.x >= Constants.GetMaxBoundX()
        ) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (
            (this.tag == "BulletEnemy" && other.tag == "Player")
            || (this.tag == "BulletPlayer" && other.tag == "Enemy")
        ) {
            Destroy(this.gameObject);
        }
    }

    public void setDamage(int damage) {
        this.damage = damage;
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    public int getDamage() {
        return this.damage;
    }
}
