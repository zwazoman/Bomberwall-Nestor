using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBombState : AIState
{
    public override void OnEnter()
    {
        Machine.GetComponent<AIBehaviour>().FindClosestBombPickupPath();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(AIState nextState)
    {
        Machine.TransitionTo(nextState);
    }
}
