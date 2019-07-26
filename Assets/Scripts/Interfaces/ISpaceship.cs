using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpaceship
{
    void hurt(int damages);
    int getHealth();
    int getTotalHealth();
    bool isAlive();
    float getFireDelay();
    float getMovementSpeed();
    int getAttack();
    ShotType getShotType();
}
