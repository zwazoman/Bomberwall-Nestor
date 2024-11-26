using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine : MonoBehaviour
{
    public AIState CurrentState;
    //public BotStates InitialState;

    public GetBombState GetBomb = new GetBombState();
    public EscapeBombState EscapeBomb = new EscapeBombState();
    public ChasePlayerState ChasePlayer = new ChasePlayerState();
    public DeadState Dead = new DeadState();
    public WinState Win = new WinState();

    public AIController Controller;
    public AISensor Sensor;

    private void Awake()
    {
        Controller = GetComponent<AIController>();
        Sensor = GetComponent<AISensor>();
    }

    private void Start()
    {
        GetBomb.Machine = this;
        EscapeBomb.Machine = this;
        ChasePlayer.Machine = this;
        Dead.Machine = this;
        Win.Machine = this;


        //set start state
        TransitionTo(GetBomb);
    }
    public void TransitionTo(AIState state)
    {
        CurrentState = state;
        state.OnEnter();
    }

    private void Update()
    {
        CurrentState.Update();
    }

}
