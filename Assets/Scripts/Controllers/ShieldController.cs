using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private Transform shiled;

    public Transform owner;
    public Vector3 shieldOffset;

    void Start()
    {
        this.shiled = GetComponent<Transform>();
        this.StartGameCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "BulletPlayer") {
            Destroy(other.gameObject);
        }
    }

    void Update () {
        if (this.owner) {
            this.shiled.position = this.owner.position + shieldOffset;
        } else {
            Destroy(this.gameObject);        
        }
    }
}
