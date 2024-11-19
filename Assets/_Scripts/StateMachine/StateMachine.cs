using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BotStates CurrentState;
    //public BotStates InitialState;

    static GetBombState _getBomb = new GetBombState();
    static EscapeBombState _escapeBomb = new EscapeBombState();
    static ChasePlayerState _chasePlayer = new ChasePlayerState();
    static DeadState _dead = new DeadState();
    static WinState _win = new WinState();

    List<BotStates> _botStates = new List<BotStates>()
    {
        _getBomb,
        _escapeBomb,
        _chasePlayer,
        _dead,
        _win
    };

    private void Start()
    {
        foreach (BotStates state in _botStates)
        {
            state.Machine = this;
        }

        //set start state
        TransitionTo(_getBomb);
    }
    public void TransitionTo(BotStates state)
    {
        CurrentState = state;
        state.OnEnter();
    }

    private void Update()
    {
        CurrentState.Update();
    }

}
