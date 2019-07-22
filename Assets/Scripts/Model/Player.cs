using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Spaceship
{
    public Player(
    ) : base (
        1000,
        100,
        0.5f,
        100f
    ) {
    }

    public void powerUp() {
        if (this.fireDelay - 0.05f > 0.1f) {
            this.fireDelay = this.fireDelay - 0.05f;
        } else {
            this.attack += 25;
        }
        this.health = Mathf.Min(this.health + 50, this.totalHealth);
    }
}
