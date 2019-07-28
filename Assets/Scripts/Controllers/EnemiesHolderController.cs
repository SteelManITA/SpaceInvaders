using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHolderController : MonoBehaviour
{
    private Transform enemiesHolder;
    private int enemies = 40;

    public GameObject enemy;

    void spawnEnemies() {
        for (int i = 0; i < this.enemies; ++i) {
            Instantiate(
                this.enemy,
                this.enemiesHolder.position + new Vector3(i/5, i%5 * 1.5f, 0) + new Vector3(0f, 11f, 0f),
                Quaternion.identity,
                this.enemiesHolder
            );
        }
    }

    void Start()
    {
        this.enemiesHolder = GetComponent<Transform>();
        this.spawnEnemies();
    }

    void Update()
    {
        if (this.enemiesHolder.childCount == 0) {
            GameState.getInstance().nextLevel();
            this.spawnEnemies();
        }
    }
}
