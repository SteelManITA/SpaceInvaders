using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy : ISpaceship
{
    float getDropRate();
    int getScore();
}
