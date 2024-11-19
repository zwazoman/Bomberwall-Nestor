using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotStates
{
    //initialisation
    public StateMachine Machine;

    //potential conditions
    protected int health;
    protected int playerHealth;
    protected int playerMaxHealth;
    protected int bombPossessedCount;
    protected int closestBombPickupDistance;
    protected int playerDistance;
    protected int bombDistance;

    //weights
    static protected int healthWheight;
    static protected int playerHealthWeight;
    static protected int bombPossessedCountWeight;
    static protected int closestBombPickupDistanceWeight;
    static protected int playerDistanceWeight;
    static protected int bombDistanceWeight;

    //outcomes


    public abstract void OnEnter();
    public abstract void Update();
    public abstract void Exit(BotStates nextState);

}    
