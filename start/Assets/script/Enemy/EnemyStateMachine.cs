using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{

    public EnemyState currentState { get; private set; } 
   
    public virtual void Initialize(EnemyState _currentState)
    {
       
        currentState = _currentState;
        currentState.Enter();
    }

    public virtual void ChangeState(EnemyState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
