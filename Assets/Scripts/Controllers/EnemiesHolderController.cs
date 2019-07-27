using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHolderController : MonoBehaviour
{
    private Transform enemiesHolder;
    private int enemies = 40;
    private GameState state;
    private Vector3 spawnOffset = new Vector3(0f, 11f, 0f);

    public GameObject enemy;
    public GameObject boss;

    void spawnEnemies() {
        for (int i = 0; i < this.enemies; ++i) {
            Instantiate(
                this.enemy,
                this.enemiesHolder.position + new Vector3(i/5, i%5 * 1.5f, 0) + spawnOffset,
                Quaternion.identity,
                this.enemiesHolder
            );
        }
    }

    void spawnBoss() {
        Instantiate(
            this.boss,
            this.enemiesHolder.position + new Vector3(3.5f, 5, 0) + spawnOffset,
            Quaternion.identity,
            this.enemiesHolder
        );
    }

    void Start()
    {
        this.enemiesHolder = GetComponent<Transform>();
        this.state = GameState.getInstance();
        this.spawnEnemies();
    }

    void Update()
    {
        if (this.enemiesHolder.childCount == 0) {
            this.state.nextLevel();
            if (this.state.getLevel() % 5 == 0) {
                this.spawnBoss();
            } else {
                this.spawnEnemies();
            }
        }
    }
}
