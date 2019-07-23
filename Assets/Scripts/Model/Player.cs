using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Spaceship
{
    private int powerUpCount = 0;
    public Player(
    ) : base (
        1000,
        100,
        0.5f,
        100f
    ) {
        // TODO: set this.type according to PlayerPref
        this.shotType = ShotType.linear;
    }

    public void powerUp() {
        ++this.powerUpCount;
        if (this.fireDelay - 0.05f > 0.1f) {
            this.fireDelay = this.fireDelay - 0.05f;
        } else {
            this.attack += 25;
        }
        this.health = Mathf.Min(this.health + 50, this.totalHealth);
    }

    public int getPowerUpCount() {
        return this.powerUpCount;
    }
}
