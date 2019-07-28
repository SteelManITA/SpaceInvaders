using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship, IEnemy
{
    protected float dropRate = 0.08f;
    protected int score = 100;

    public Enemy()
    {
    }

    public Enemy(
        int level
    ) {
        this.health = this.totalHealth = (int) (200 * (1.1f * level));
        this.attack = 100 + (15 * level);
        this.fireDelay = this.setFireDelay(level);
        this.movementSpeed = 0.5f;
    }

    private float setFireDelay(int level)
    {
        // level 1 return 33
        // level 5 return 5
        // level 30 return 1.5
        float rate = 0.25f * Mathf.Log(level, Constants.euler) + 33.63f / level;
        // Debug.Log("Level: " + level + " Rate: " + rate);

        // Per evitare eventuali crash su livelli troppo alti
        return rate < 100f ? rate : 1.5f;
    }

    public float getDropRate() {
        return this.dropRate;
    }

    public int getScore() {
        return this.score;
    }
}
