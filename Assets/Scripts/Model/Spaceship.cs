using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship
{
    protected int health;
    protected int totalHealth;

    public Spaceship(
        int totalHealth
    ) {
        this.health = this.totalHealth = totalHealth;
    }

    public void hurt(int damages) {
        Debug.Log("hurt: " + this.health + " - " + damages + " / " + this.totalHealth);
        if (damages < 0) {
            damages = -damages;
        }
        this.health = Mathf.Max(0, this.health - damages);
    }

    public int getHealth() {
        return this.health;
    }

    public int getTotalHealth() {
        return this.totalHealth;
    }

    public bool isAlive() {
        return this.health > 0;
    }
}
