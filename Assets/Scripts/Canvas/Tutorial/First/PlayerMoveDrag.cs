using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveDrag : MonoBehaviour
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
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;
            (GetComponent<Rigidbody2D>()).velocity = new Vector2(direction.x * this.model.getMovementSpeed(), direction.y * this.model.getMovementSpeed());
        } else {
            (GetComponent<Rigidbody2D>()).velocity = Vector2.zero;
        }

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
