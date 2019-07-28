using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitBoss : MonoBehaviour
{
    public GameObject model;

    void Awake()
    {
        GetComponent<Renderer>().material.color = Color.green;
        BulletController controller = GetComponent<BulletController>();
        controller.setSpeed(-0.3f);
        controller.tag = "BulletPlayer";
    }

    void Respawn()
    {
        GameObject go = Instantiate(
            this.model,
            new Vector3(0, -7f, 0),
            Quaternion.identity,
            this.GetComponent<Transform>().parent
        );
        BulletHitBoss hitBoss = go.AddComponent<BulletHitBoss>();
        hitBoss.model = this.model;
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy" || other.tag == "Shield") {
            this.Respawn();
            Destroy(this.gameObject);
        }
    }
}
