using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : ISpaceship
{
    void powerUp();
    int getPowerUpCount();
}
