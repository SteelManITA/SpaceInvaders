using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitted : MonoBehaviour
{
    public GameObject shield;
    int hitCount = 0;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "BulletPlayer") {
            ++hitCount;
            if (hitCount%3 != 0) {
                return;
            }
            
            Vector3 shieldOffset = new Vector3(0, -1.5f, 0);
            GameObject shield = Instantiate(
                this.shield,
                GetComponent<Transform>().position + shieldOffset,
                Quaternion.identity,
                this.GetComponent<Transform>().parent
            );
            ShieldController sh = shield.AddComponent<ShieldController>();
            sh.owner = GetComponent<Transform>();
            sh.shieldOffset = shieldOffset;
        }
    }
}
