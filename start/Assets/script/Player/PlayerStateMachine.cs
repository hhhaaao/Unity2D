using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }//ȡʱ��������ʱ˽��

    public virtual void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public virtual void ChangeState(PlayerState _newState)
    {  
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
