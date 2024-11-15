using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState CurrentState;

    public BaseState InitialState;

    public GetBombState GetBomb;
    public EscapeBombState EscapeBomb;
    public ChasePlayerState ChasePlayer;

    public void TransitionTo(BaseState state)
    {
        CurrentState = state;
        state.OnEnter();
    }

    private void Update()
    {
        CurrentState.Update();
    }

}
