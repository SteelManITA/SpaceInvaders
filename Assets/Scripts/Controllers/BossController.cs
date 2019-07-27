using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    private float[] hittedTime = new float[3];
    private float lastTime;


    protected Transform player;

    public GameObject shield;

    override protected void Awake()
    {
        base.Awake();
        this.model = new Boss(this.state.getLevel());
        this.hittedTime[0] = this.hittedTime[1] = this.hittedTime[2] = -10f;
        this.lastTime = UnityEngine.Time.time;
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    override protected IEnumerator Shot()
    {
        yield return WaitForShot(1.0f + Random.value * this.model.getFireDelay());

        while (player) {
            Vector3 p1 = this.enemy.position;
            Vector3 p2 = player.position;
            float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
            Vector3 rotationVector = new Vector3(0, 0, angle + 90);
            Quaternion pos = Quaternion.Euler(rotationVector);
            GameObject shotInstance = Instantiate(
                this.shot,
                this.enemy.position,
                pos
            );
            shotInstance.GetComponent<Renderer>().material.color = Color.red;
            BulletController controller = shotInstance.GetComponent<BulletController>();
            controller.setDamage(this.model.getAttack());
            controller.setSpeed(0.1f);
            controller.tag = "BulletEnemy";

            yield return WaitForShot(this.model.getFireDelay());
        }
    }

    void ActivateShield()
    {
        Vector3 shieldOffset = new Vector3(0, -1.5f, 0);
        GameObject shield = Instantiate(
            this.shield,
            this.enemy.position + shieldOffset,
            this.enemy.rotation
        );
        ShieldController sh = shield.AddComponent<ShieldController>();
        sh.owner = this.enemy;
        sh.shieldOffset = shieldOffset;
    }

    override protected void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if (other.tag == "BulletPlayer") {
            int older = 0;
            for (int i = 1; i < 3; ++i) {
                older = this.hittedTime[i] < this.hittedTime[older] ? i : older;
            }

            this.hittedTime[older] = UnityEngine.Time.time;
            this.lastTime = UnityEngine.Time.time;

            for (int i = 0; i < 3; ++i) {
                if (UnityEngine.Time.time - this.hittedTime[i] > 2f) {
                    return;
                }
            }
            ActivateShield();
        }
    }
}
