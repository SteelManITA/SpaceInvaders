using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAccelerometer : MonoBehaviour
{
    private Transform player;
    private IPlayer model;

    public float minBound;
    public float maxBound;

    void Start()
    {
        this.player = GetComponent<Transform>();
        this.model = new SpaceShooter();
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration * 0.3f;
        (GetComponent<Rigidbody2D>()).velocity = new Vector2(acceleration.x * this.model.getMovementSpeed(), acceleration.y * this.model.getMovementSpeed());

        if (
            this.player.position.x < this.minBound
            || this.player.position.x > this.maxBound
        ) {
            this.player.position = new Vector3(
                this.player.position.x < this.minBound ? this.minBound : this.maxBound,
                this.player.position.y,
                this.player.position.z
            );
        }
    }
}
