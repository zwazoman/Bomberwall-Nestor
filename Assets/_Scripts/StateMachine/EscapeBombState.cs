using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBombState : AIState
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
}
