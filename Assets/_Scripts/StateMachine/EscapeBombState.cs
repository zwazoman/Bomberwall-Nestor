using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBombState : BotStates
{
    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(BotStates nextState)
    {
        Machine.TransitionTo(nextState);
    }
}
