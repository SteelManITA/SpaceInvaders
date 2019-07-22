using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
{
    private float dropRate = 0.1f;

    public Enemy(
        int level
    ) : base (
        setTotalHealth(level),
        setAttack(level),
        setFireDelay(level),
        setMovementSpeed(level)
    ) {
    }

    private static int setTotalHealth(int level)
    {
        return (int) (200 * (1.1f * level));
    }

    private static int setAttack(int level)
    {
        return 100;
    }

    private static float setFireDelay(int level)
    {
        // level 1 return 33
        // level 5 return 5
        // level 30 return 1.5
        float rate = 0.25f * Mathf.Log(level, Constants.euler) + 33.63f / level;
        // Debug.Log("Level: " + level + " Rate: " + rate);

        // Per evitare eventuali crash su livelli troppo alti
        return rate < 100f ? rate : 1.5f;
    }

    private static float setMovementSpeed(int level)
    {
        return 0.5f;
    }

    public float getDropRate() {
        return this.dropRate;
    }
}
