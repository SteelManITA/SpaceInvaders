using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship
{
    protected int health;
    protected int totalHealth;
    protected int attack;
    protected float fireDelay; // shoot per second
    protected float movementSpeed;

    public Spaceship(
        int totalHealth,
        int attack,
        float fireDelay,
        float movementSpeed
    ) {
        this.health = this.totalHealth = totalHealth;
        this.attack = attack;
        this.fireDelay = fireDelay;
        this.movementSpeed = movementSpeed;
    }

    public void hurt(int damages) {
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

    public float getFireDelay() {
        return this.fireDelay;
    }

    public float getMovementSpeed() {
        return this.movementSpeed;
    }

    public int getAttack() {
        return this.attack;
    }
}
