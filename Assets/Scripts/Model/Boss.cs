using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Movement {
    public Vector2 direction;
    public float time;

    public Movement(
        Vector2 direction,
        float time
    ) {
        this.direction = direction;
        this.time = time;
    }
}


public class Boss : Enemy
{
    public Boss(
        int level
    ) {
        this.health = this.totalHealth = 1000 * level;
        this.attack = 100 + (15 * level);
        this.fireDelay = Mathf.Max(0.2f, 1f - ((level/5) - 1) * 0.2f);
        this.movementSpeed = 0.5f;
        this.dropRate = .08f;
        this.score = 1000;
    }

    // Vector2 direction, float time
    public Movement[] getMovementPattern()
    {
        Movement[] movements = {
            new Movement(
                new Vector2(.5f, 0),
                1f
            ),
            new Movement(
                new Vector2(2f, 0),
                1f
            ),
            new Movement(
                new Vector2(-1f, 0),
                1f
            ),
            new Movement(
                new Vector2(-4f, 0),
                1f
            ),
            new Movement(
                new Vector2(.5f, 0),
                1f
            )
        };

        return movements;
    }
}
