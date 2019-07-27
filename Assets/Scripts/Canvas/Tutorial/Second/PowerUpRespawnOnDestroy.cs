using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRespawnOnDestroy : MonoBehaviour
{
    public GameObject model;
    public float minBoundX;
    public float maxBoundX;
    public float maxBoundY;

    void Respawn()
    {
        GameObject go = Instantiate(
            this.model,
            new Vector3(Random.Range(this.minBoundX, this.maxBoundX), 2.5f, 0),
            Quaternion.identity,
            this.GetComponent<Transform>().parent
        );
        PowerUpRespawnOnDestroy component = go.AddComponent<PowerUpRespawnOnDestroy>();
        component.model = this.model;
        component.minBoundX = this.minBoundX;
        component.maxBoundX = this.maxBoundX;
        component.maxBoundY = this.maxBoundY;
    }

    void Update() {
        if (this.GetComponent<Transform>().position.y <= this.maxBoundY) {
            this.Respawn();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            this.Respawn();
            Destroy(this.gameObject);
        }
    }
}
