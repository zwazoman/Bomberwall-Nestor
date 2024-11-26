using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    //initialisation
    public StateMachine Machine;

    //potential conditions
    protected int health;
    protected int playerHealth;
    protected bool hasBombs;

    //weights
    protected int healthWheight;
    protected int playerHealthWeight;
    protected int bombPossessedCountWeight;
    protected int closestBombPickupDistanceWeight;
    protected int playerDistanceWeight;
    protected int bombDistanceWeight;

    //outcomes


    public abstract void OnEnter();
    public abstract void Update();
    public abstract void Exit(AIState nextState);

}    
