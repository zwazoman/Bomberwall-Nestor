using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    //potential conditions
    int health;
    int playerHealth;
    int bombPossessedCount;
    int closestBombPickupDistance;
    int playerDistance;
    int bombDistance;

    //weights
    int healthWheight;
    int playerHealthWeight;
    int bombPossessedCountWeight;
    int closestBombPickupDistanceWeight;
    int playerDistanceWeight;
    int bombDistanceWeight;

    //outcomes

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Update();
}
