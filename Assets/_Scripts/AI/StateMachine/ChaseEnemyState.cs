using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerState : AIState
{
    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(AIState nextState)
    {
        Machine.TransitionTo(nextState);
    }

    public override bool CanEnter()
    {
        throw new System.NotImplementedException();
    }
}
